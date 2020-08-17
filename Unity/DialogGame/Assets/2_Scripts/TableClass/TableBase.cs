using System.Collections;
using System.Collections.Generic;

public abstract class TableBase
{
    // 해당 테이블의 정보를 저장하는 곳
    protected Dictionary<string, Dictionary<string, string>> _sheetData;

    /// <summary>
    /// 테이블 파일을 정보를 읽어와서 저장소에 저장하는 함수
    /// </summary>
    /// <param name="strJson">json파일명</param>
    public abstract void LoadTable(string strJson);

    public void Add(string key, string subKey, string val)
    {
        if(!_sheetData.ContainsKey(key))
        {
            // Save
            _sheetData.Add(key, new Dictionary<string, string>());
        }

        if(!_sheetData[key].ContainsKey(subKey))
        {
            _sheetData[key].Add(subKey, val);
        }
    }

    public string GetToStr(int index, string columnName)
    {
        return _sheetData[index.ToString()][columnName];
    }
}
