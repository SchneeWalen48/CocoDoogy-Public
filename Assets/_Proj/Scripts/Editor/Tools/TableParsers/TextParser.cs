using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class TextData
{
    public int id;
    public string text;
}

public static class TextParser
{
    // CSV를 읽어 Dictionary<string, string> 형태로 반환
    public static Dictionary<string, string> Import(string csvPath)
    {
        var dict = new Dictionary<string, string>();
        string[] lines = File.ReadAllLines(csvPath);

        if (lines.Length <= 1)
        {
            Debug.LogWarning("[TextParser] CSV에 데이터가 없습니다.");
            return dict;
        }

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            if (string.IsNullOrWhiteSpace(line)) continue;

            // 따옴표 포함 방지
            string[] v = line.Split(',');

            // 최소 두 컬럼: kr_text_id, kr_text
            if (v.Length < 2)
                continue;

            string key = v[0].Trim().Trim('"');
            string value = v[1].Trim().Trim('"');

            if (!dict.ContainsKey(key))
                dict[key] = value;
            else
                Debug.LogWarning($"[TextParser] 중복된 키 발견 → {key}");
        }

        Debug.Log($"[TextParser] 총 {dict.Count}개의 문자열 로드 완료");
        return dict;
    }

    // [key] 형식 문자열을 실제 텍스트로 변환하는 헬퍼 함수
    public static string Resolve(string raw, Dictionary<string, string> dict)
    {
        if (string.IsNullOrEmpty(raw))
            return raw;

        if (raw.StartsWith("[") && raw.EndsWith("]"))
        {
            string key = raw.Trim('[', ']');
            if (dict.TryGetValue(key, out string text))
                return text;
        }

        return raw; // 일반 문자열 그대로 반환
    }
}