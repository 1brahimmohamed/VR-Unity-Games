using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public static class SavWav
{
    const int HEADER_SIZE = 44;

    public static bool Save(string filename, AudioClip clip)
    {
        if (!filename.ToLower().EndsWith(".wav"))
        {
            filename += ".wav";
        }

        var filepath = Path.Combine(Application.persistentDataPath, filename);

        var samples = new float[clip.samples];
        clip.GetData(samples, 0);

        using (var fileStream = CreateEmpty(filepath))
        {
            ConvertAndWrite(fileStream, samples);
            WriteHeader(fileStream, clip);
        }

        return true;
    }

    static FileStream CreateEmpty(string filepath)
    {
        var fileStream = new FileStream(filepath, FileMode.Create);
        byte emptyByte = new byte();

        for (int i = 0; i < HEADER_SIZE; i++) //preparing the header
        {
            fileStream.WriteByte(emptyByte);
        }

        return fileStream;
    }

    static void ConvertAndWrite(FileStream fileStream, float[] samples)
    {
        var intData = new short[samples.Length];

        for (int i = 0; i < samples.Length; i++)
        {
            intData[i] = (short)(samples[i] * 32767);
        }

        var byteArray = new byte[intData.Length * 2];
        System.Buffer.BlockCopy(intData, 0, byteArray, 0, byteArray.Length);
        fileStream.Write(byteArray, 0, byteArray.Length);
    }

    static void WriteHeader(FileStream fileStream, AudioClip clip)
    {
        var hz = clip.frequency;
        var channels = clip.channels;
        var samples = clip.samples;

        fileStream.Seek(0, SeekOrigin.Begin);

        var riff = System.Text.Encoding.UTF8.GetBytes("RIFF");
        fileStream.Write(riff, 0, 4);

        var chunkSize = System.BitConverter.GetBytes(fileStream.Length - 8);
        fileStream.Write(chunkSize, 0, 4);

        var wave = System.Text.Encoding.UTF8.GetBytes("WAVE");
        fileStream.Write(wave, 0, 4);

        var fmt = System.Text.Encoding.UTF8.GetBytes("fmt ");
        fileStream.Write(fmt, 0, 4);

        var subChunk1 = System.BitConverter.GetBytes(16);
        fileStream.Write(subChunk1, 0, 4);

        ushort one = 1;
        var audioFormat = System.BitConverter.GetBytes(one);
        fileStream.Write(audioFormat, 0, 2);

        var numChannels = System.BitConverter.GetBytes(channels);
        fileStream.Write(numChannels, 0, 2);

        var sampleRate = System.BitConverter.GetBytes(hz);
        fileStream.Write(sampleRate, 0, 4);

        var byteRate = System.BitConverter.GetBytes(hz * channels * 2); // sampleRate * bytesPerSample*number of channels, here 44100*2*2
        fileStream.Write(byteRate, 0, 4);

        ushort blockAlign = (ushort)(channels * 2);
        fileStream.Write(System.BitConverter.GetBytes(blockAlign), 0, 2);

        ushort bps = 16;
        var bitsPerSample = System.BitConverter.GetBytes(bps);
        fileStream.Write(bitsPerSample, 0, 2);

        var dataString = System.Text.Encoding.UTF8.GetBytes("data");
        fileStream.Write(dataString, 0, 4);

        var subChunk2 = System.BitConverter.GetBytes(samples * channels * 2);
        fileStream.Write(subChunk2, 0, 4);

        fileStream.Close();
    }
}
