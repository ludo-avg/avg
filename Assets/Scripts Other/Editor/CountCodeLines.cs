using System;
using System.IO;
using UnityEngine;
using UnityEditor;

/*
    来自 https://blog.csdn.net/LLLLL__/article/details/104288456的
*/
public class CountCodeLines
{
    [MenuItem("Tools/Code Line Count")]
    private static void PrintTotalLine()
    {
        int scripts_lines = 0;
        {
            string[] fileName = Directory.GetFiles("Assets/Scripts", "*.cs", SearchOption.AllDirectories);
            int totalLine = 0;
            foreach (var temp in fileName)
            {
                int nowLine = 0;
                StreamReader sr = new StreamReader(temp);
                while (sr.ReadLine() != null)
                {
                    nowLine++;
                }

                totalLine += nowLine;
            }
            scripts_lines = totalLine;
        }
        int other_lines;
        {
            string[] fileName = Directory.GetFiles("Assets/Scripts Other", "*.cs", SearchOption.AllDirectories);
            int totalLine = 0;
            foreach (var temp in fileName)
            {
                int nowLine = 0;
                StreamReader sr = new StreamReader(temp);
                while (sr.ReadLine() != null)
                {
                    nowLine++;
                }

                totalLine += nowLine;
            }
            other_lines = totalLine;
            
        }
        Debug.Log(String.Format("Script行数：{0}", scripts_lines));
        Debug.Log(String.Format("Other行数：{0}", other_lines));
        Debug.Log(String.Format("总行数：{0}", scripts_lines + other_lines));
    }
}