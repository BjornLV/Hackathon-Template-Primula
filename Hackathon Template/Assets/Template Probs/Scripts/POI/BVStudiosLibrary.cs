using System;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;

namespace BVStudios
{
    public static class StyleText
    {
        public static string Colorize(string text, string color, bool bold)
        {
            return
            "<color=" + color + ">" +
            (bold ? "<b>" : "") +
            text +
            (bold ? "</b>" : "") +
            "</color>";
        }
    }

    [System.Serializable]
    public enum TimerType
    {
        None,
        Elapsed,
        Countdown
    }

    public static class SaveSystem
    {
        public static string CreateValidName(string name)
        {
            char[] invalidChars = Path.GetInvalidFileNameChars();
            Array.Resize(ref invalidChars, invalidChars.Length + 2);
            invalidChars[invalidChars.Length - 2] = ':';
            invalidChars[invalidChars.Length - 1] = '_';

            string invalidRegex = string.Format("[{0}]", Regex.Escape(new string(invalidChars)));

            string validName = Regex.Replace(name, invalidRegex, "_");

            validName = validName.Replace(" ", "");

            return validName;
        }

        public static void SaveData<T>(string saveName, T data)
        {
            saveName = CreateValidName(saveName);

            BinaryFormatter formatter = new BinaryFormatter();
            string savePath = GetSavePath(saveName);
            FileStream stream = new FileStream(savePath, FileMode.Create);
            Debug.Log(savePath);
            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static T LoadData<T>(string saveName)
        {
            saveName = CreateValidName(saveName);

            string savePath = GetSavePath(saveName);
            if (File.Exists(savePath))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(savePath, FileMode.Open);

                T data = (T)formatter.Deserialize(stream);
                stream.Close();

                return data;
            }
            else
            {
                Debug.LogError("Save file not found in " + savePath);
                return default(T);
            }
        }

        public static void ListData()
        {
            string[] files = Directory.GetFiles(Application.persistentDataPath, "*.sav");
            if (files.Length == 0)
            {
                Debug.Log("No save files found.");
            }
            else
            {
                Debug.Log("Saved files:");
                foreach (string file in files)
                {
                    Debug.Log(Path.GetFileNameWithoutExtension(file));
                }
            }
        }

        public static void Wipe()
        {
            string[] files = Directory.GetFiles(Application.persistentDataPath, "*.sav");
            if (files.Length == 0)
            {
                Debug.Log("No save files found.");
            }
            else
            {
                foreach (string file in files)
                {
                    File.Delete(file);
                }
            }
        }

        public static bool DoesSaveExist(string saveName)
        {
            saveName = CreateValidName(saveName);

            string savePath = GetSavePath(saveName);
            return File.Exists(savePath);
        }

        public static void DeleteData(string saveName)
        {
            saveName = CreateValidName(saveName);

            string savePath = GetSavePath(saveName);
            if (File.Exists(savePath))
            {
                File.Delete(savePath);
            }
        }

        public static string GetSavePath(string saveName)
        {
            saveName = CreateValidName(saveName);

            return Application.persistentDataPath + "/" + saveName + ".sav";
        }
    }
}