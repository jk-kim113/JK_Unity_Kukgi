using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortClass
{
    public static IEnumerator SeletedSort(int[] arry, bool isAscend = false)
    {
        for(int n = 0; n < arry.Length - 1; n++)
        {
            for(int m = n + 1; m < arry.Length; m++)
            {
                if (!isAscend)
                {
                    if (arry[n] < arry[m])
                    {
                        yield return new WaitForSeconds(1.5f);
                        SortTest._instance.PrintState("내림 차순");
                        
                        int temp = arry[m];
                        arry[m] = arry[n];
                        arry[n] = temp;
                    }
                }
                else
                {
                    if (arry[n] > arry[m])
                    {
                        yield return new WaitForSeconds(1.5f);
                        SortTest._instance.PrintState("오름 차순");
                        
                        int temp = arry[m];
                        arry[m] = arry[n];
                        arry[n] = temp;
                    }
                }

                SortTest._instance.PrintArray();
            }
        }

        SortTest._instance.PrintState("정렬 끝");
    }

    public static IEnumerator InsertSort(int[] arry, bool isAscend = false)
    {
        int currentIdx = 0;

        for(int n = 1; n < arry.Length; n++)
        {
            currentIdx = n;

            if(!isAscend)
            {
                if (arry[currentIdx] < arry[currentIdx - 1])
                {
                    continue;
                }
            }
            else
            {
                if (arry[currentIdx] > arry[currentIdx - 1])
                {
                    continue;
                }
            }

            for(int m = n - 1; m >= 0; m--)
            {
                if(!isAscend)
                {
                    if (arry[currentIdx] > arry[m])
                    {
                        SortTest._instance.PrintState("내림 차순");
                        yield return new WaitForSeconds(1.5f);

                        int temp = arry[m];
                        arry[m] = arry[currentIdx];
                        arry[currentIdx] = temp;

                        currentIdx = m;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    if (arry[currentIdx] < arry[m])
                    {
                        SortTest._instance.PrintState("오름 차순");
                        yield return new WaitForSeconds(1.5f);

                        int temp = arry[m];
                        arry[m] = arry[currentIdx];
                        arry[currentIdx] = temp;

                        currentIdx = m;
                    }
                    else
                    {
                        break;
                    }
                }

                SortTest._instance.PrintArray();
            }
        }

        SortTest._instance.PrintState("정렬 끝");
    }

    public static IEnumerator InsertionSort(int[] arry, bool isAscend = false)
    {
        for(int n = 1; n < arry.Length; n++)
        {
            int std = arry[n], m = n - 1;

            yield return new WaitForSeconds(1.5f);

            if(isAscend)
            {
                while(m >= 0 && std < arry[m])
                {
                    int t = arry[m + 1];
                    arry[m + 1] = arry[m];
                    arry[m] = t;
                    m--;
                    yield return new WaitForSeconds(1.5f);
                }

                arry[m + 1] = std;
            }
            else
            {
                while (m >= 0 && std > arry[m])
                {
                    int t = arry[m + 1];
                    arry[m + 1] = arry[m];
                    arry[m] = t;
                    m--;
                    yield return new WaitForSeconds(1.5f);
                }

                arry[m + 1] = std;
            }
        }
    }

    public static IEnumerator BubbleSort(int[] arry, bool isAscend = false)
    {
        for(int m = 0; m < arry.Length - 1; m++)
        {
            for (int n = 1; n < arry.Length - m; n++)
            {
                if(!isAscend)
                {
                    SortTest._instance.PrintState("내림 차순");

                    if (arry[n] > arry[n - 1])
                    {
                        yield return new WaitForSeconds(1.5f);
                        int temp = arry[n];
                        arry[n] = arry[n - 1];
                        arry[n - 1] = temp;
                    }
                }
                else
                {
                    SortTest._instance.PrintState("오름 차순");

                    if (arry[n] < arry[n - 1])
                    {
                        yield return new WaitForSeconds(1.5f);
                        int temp = arry[n];
                        arry[n] = arry[n - 1];
                        arry[n - 1] = temp;
                    }
                }

                SortTest._instance.PrintArray();
            }
        }

        SortTest._instance.PrintState("정렬 끝");
    }

    public static void MergeSort(int[] arry, int start, int end, bool isAscend = false)
    {
        if(start < end)
        {
            int m = (start + end) / 2;

            MergeSort(arry, start, m);
            MergeSort(arry, m + 1, end);

            Merge(arry, start, end, m, isAscend);
        }
    }

    static void Merge(int[] arry, int start, int end, int merge, bool isAscend = false)
    {
        List<int> tempList = new List<int>();
        int i = start, j = merge + 1, copy = 0;

        while(i <= merge && j <= end)
        {
            if(arry[i] < arry[j])
            {
                if (!isAscend)
                    tempList.Add(arry[j++]);
                else
                    tempList.Add(arry[i++]);

            }
            else if(arry[i] > arry[j])
            {
                if (!isAscend)
                    tempList.Add(arry[i++]);
                else
                    tempList.Add(arry[j++]);
            }
        }

        while (i <= merge)
        {
            tempList.Add(arry[i++]);
        }
        while (j <= end)
        {
            tempList.Add(arry[j++]);
        }

        for (int n = start; n <= end; n++)
        {
            arry[n] = tempList[copy++];
        }
    }

    public static void QuickSort(int[] arry, int start, int end, bool isAscend = false)
    {
        int pivot = arry[start];
        int bs = start, be = end;

        while(start < end)
        {
            while(pivot <= arry[end] && start < end)
            {
                end--;
            }

            if (start > end)
                break;

            while(pivot >= arry[start] && start < end)
            {
                start++;
            }

            if (start > end)
                break;

            int temp = arry[start];
            arry[start] = arry[end];
            arry[end] = temp;
        }

        int change = arry[bs];
        arry[bs] = arry[start];
        arry[start] = change;

        if (bs < start)
            QuickSort(arry, bs, start - 1, isAscend);
        if(be > end)
            QuickSort(arry, start + 1, be, isAscend);
    }
}

/* 정렬 알고리즘
 *      정렬 알고리즘의 종류
 *          비교 정렬 알고리즘
 *              삽입, 선택, 퀵, 분열 합병, 셀 등이 있다.
 *          비교하지 않는 알고리즘
 *              비둘기집 정렬, 버킷 정렬, LSD기수 정렬 등이 있다.
 *          기타 정렬 알고리즘
 *              스파게티 정렬, 바이토닉 정렬 등이 있다.
 */
