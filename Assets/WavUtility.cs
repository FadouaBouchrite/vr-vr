using UnityEngine;
using System;

public static class WavUtility
{
    const int HEADER_SIZE = 44;

    public static AudioClip ToAudioClip(byte[] wavData, string clipName = "")
    {
        if (wavData == null || wavData.Length < HEADER_SIZE)
            throw new ArgumentException("Invalid WAV data");

        // Check the WAV file header
        string chunkID = System.Text.Encoding.ASCII.GetString(wavData, 0, 4);
        if (chunkID != "RIFF")
            throw new ArgumentException("Invalid WAV file: Incorrect ChunkID");

        string format = System.Text.Encoding.ASCII.GetString(wavData, 8, 4);
        if (format != "WAVE")
            throw new ArgumentException("Invalid WAV file: Incorrect Format");

        // Extract header
        int channels = BitConverter.ToInt16(wavData, 22);
        int sampleRate = BitConverter.ToInt32(wavData, 24);
        int bitsPerSample = BitConverter.ToInt16(wavData, 34);
        int dataSize = BitConverter.ToInt32(wavData, 40);

        // Log the extracted information for debugging
        Debug.Log($"Channels: {channels}, Sample Rate: {sampleRate}, Bits per Sample: {bitsPerSample}, Data Size: {dataSize}");

        // Check for supported bit depths
        if (bitsPerSample != 16 && bitsPerSample != 8 && bitsPerSample != 32)
            throw new ArgumentException("Only 8-bit, 16-bit, and 32-bit WAV files are supported");

        // Load PCM data
        float[] data = new float[dataSize / (bitsPerSample / 8)];
        int offset = HEADER_SIZE;
        for (int i = 0; i < data.Length; i++)
        {
            if (bitsPerSample == 16)
                data[i] = BitConverter.ToInt16(wavData, offset) / 32768.0f;
            else if (bitsPerSample == 8)
                data[i] = (wavData[offset] - 128) / 128.0f;
            else if (bitsPerSample == 32)
                data[i] = BitConverter.ToInt32(wavData, offset) / 2147483648.0f;
            offset += bitsPerSample / 8;
        }

        // Create AudioClip
        AudioClip audioClip = AudioClip.Create(clipName, data.Length, channels, sampleRate, false);
        audioClip.SetData(data, 0);
        return audioClip;
    }

    public static void FromAudioClip(AudioClip audioClip, out byte[] wavData)
    {
        if (audioClip == null)
            throw new ArgumentException("Invalid audioClip");

        int channels = audioClip.channels;
        int sampleRate = audioClip.frequency;
        int samples = audioClip.samples;

        float[] data = new float[samples * channels];
        audioClip.GetData(data, 0);

        // Convert PCM data to byte array
        byte[] byteData = new byte[samples * channels * sizeof(float)];
        Buffer.BlockCopy(data, 0, byteData, 0, byteData.Length);

        // Create WAV header
        byte[] header = CreateWavHeader(channels, sampleRate, byteData.Length);

        // Combine header and data
        wavData = new byte[header.Length + byteData.Length];
        Buffer.BlockCopy(header, 0, wavData, 0, header.Length);
        Buffer.BlockCopy(byteData, 0, wavData, header.Length, byteData.Length);
    }

    static byte[] CreateWavHeader(int channels, int sampleRate, int dataLength)
    {
        int totalSize = dataLength + HEADER_SIZE - 8;
        int byteRate = sampleRate * channels * sizeof(float);
        byte[] header = new byte[HEADER_SIZE];

        // ChunkID (Riff header)
        header[0] = (byte)'R';
        header[1] = (byte)'I';
        header[2] = (byte)'F';
        header[3] = (byte)'F';

        // ChunkSize
        header[4] = (byte)(totalSize & 0xff);
        header[5] = (byte)((totalSize >> 8) & 0xff);
        header[6] = (byte)((totalSize >> 16) & 0xff);
        header[7] = (byte)((totalSize >> 24) & 0xff);

        // Format (WAVE)
        header[8] = (byte)'W';
        header[9] = (byte)'A';
        header[10] = (byte)'V';
        header[11] = (byte)'E';

        // Subchunk1ID (fmt header)
        header[12] = (byte)'f';
        header[13] = (byte)'m';
        header[14] = (byte)'t';
        header[15] = (byte)' ';
        // Subchunk1Size (16 for PCM)
        header[16] = 16;
        header[17] = 0;
        header[18] = 0;
        header[19] = 0;

        // AudioFormat (1 for PCM)
        header[20] = 1;
        header[21] = 0;

        // NumChannels
        header[22] = (byte)channels;
        header[23] = 0;

        // SampleRate
        header[24] = (byte)(sampleRate & 0xff);
        header[25] = (byte)((sampleRate >> 8) & 0xff);
        header[26] = (byte)((sampleRate >> 16) & 0xff);
        header[27] = (byte)((sampleRate >> 24) & 0xff);

        // ByteRate
        header[28] = (byte)(byteRate & 0xff);
        header[29] = (byte)((byteRate >> 8) & 0xff);
        header[30] = (byte)((byteRate >> 16) & 0xff);
        header[31] = (byte)((byteRate >> 24) & 0xff);

        // BlockAlign
        short blockAlign = (short)(channels * sizeof(float));
        header[32] = (byte)(blockAlign & 0xff);
        header[33] = (byte)((blockAlign >> 8) & 0xff);

        // BitsPerSample
        header[34] = 16;  // 16 bits per sample
        header[35] = 0;

        // Subchunk2ID (data header)
        header[36] = (byte)'d';
        header[37] = (byte)'a';
        header[38] = (byte)'t';
        header[39] = (byte)'a';

        // Subchunk2Size
        header[40] = (byte)(dataLength & 0xff);
        header[41] = (byte)((dataLength >> 8) & 0xff);
        header[42] = (byte)((dataLength >> 16) & 0xff);
        header[43] = (byte)((dataLength >> 24) & 0xff);

        return header;
    }
}
