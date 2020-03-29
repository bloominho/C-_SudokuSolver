using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace Database
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger logger = new Logger();

            List<Part> totalList = new List<Part>();
            readNumbers(totalList, 3);            //시험용 데이터 입력
            logger.write("All numbers read");
            List<Part> list10 = new List<Part>();
            List<Part> list20 = new List<Part>();
            List<Part> list30 = new List<Part>();
            List<Part> list40 = new List<Part>();
            List<Part> list50 = new List<Part>();
            List<Part> list60 = new List<Part>();
            List<Part> list70 = new List<Part>();
            List<Part> list80 = new List<Part>();
            List<Part> list90 = new List<Part>();
            List<Part> list01 = new List<Part>();
            List<Part> list02 = new List<Part>();
            List<Part> list03 = new List<Part>();
            List<Part> list04 = new List<Part>();
            List<Part> list05 = new List<Part>();
            List<Part> list06 = new List<Part>();
            List<Part> list07 = new List<Part>();
            List<Part> list08 = new List<Part>();
            List<Part> list09 = new List<Part>();
            List<Part> list001 = new List<Part>();
            List<Part> list002 = new List<Part>();
            List<Part> list003 = new List<Part>();
            List<Part> list004 = new List<Part>();
            List<Part> list005 = new List<Part>();
            List<Part> list006 = new List<Part>();
            List<Part> list007 = new List<Part>();
            List<Part> list008 = new List<Part>();
            List<Part> list009 = new List<Part>();

            List<List<Part>> lists = new List<List<Part>>() { list01, list02, list03, list04, list05, list06, list07, list08, list09, list10, list20, list30, list40, list50, list60, list70, list80, list90, list001, list002, list003, list004, list005, list006, list007, list008, list009 };

            //list01~list90까지 정리하기!
            foreach (Part p in totalList)
            {
                if (p.x == 1)
                    list01.Add(p);
                if (p.x == 2)
                    list02.Add(p);
                if (p.x == 3)
                    list03.Add(p);
                if (p.x == 4)
                    list04.Add(p);
                if (p.x == 5)
                    list05.Add(p);
                if (p.x == 6)
                    list06.Add(p);
                if (p.x == 7)
                    list07.Add(p);
                if (p.x == 8)
                    list08.Add(p);
                if (p.x == 9)
                    list09.Add(p);
                if (p.y == 1)
                    list10.Add(p);
                if (p.y == 2)
                    list20.Add(p);
                if (p.y == 3)
                    list30.Add(p);
                if (p.y == 4)
                    list40.Add(p);
                if (p.y == 5)
                    list50.Add(p);
                if (p.y == 6)
                    list60.Add(p);
                if (p.y == 7)
                    list70.Add(p);
                if (p.y == 8)
                    list80.Add(p);
                if (p.y == 9)
                    list90.Add(p);
                if (1 <= p.x && p.x <= 3 && 1 <= p.y && p.y <= 3)
                    list001.Add(p);
                if (4 <= p.x && p.x <= 6 && 1 <= p.y && p.y <= 3)
                    list002.Add(p);
                if (7 <= p.x && p.x <= 9 && 1 <= p.y && p.y <= 3)
                    list003.Add(p);
                if (1 <= p.x && p.x <= 3 && 4 <= p.y && p.y <= 6)
                    list004.Add(p);
                if (4 <= p.x && p.x <= 6 && 4 <= p.y && p.y <= 6)
                    list005.Add(p);
                if (7 <= p.x && p.x <= 9 && 4 <= p.y && p.y <= 6)
                    list006.Add(p);
                if (1 <= p.x && p.x <= 3 && 7 <= p.y && p.y <= 9)
                    list007.Add(p);
                if (4 <= p.x && p.x <= 6 && 7 <= p.y && p.y <= 9)
                    list008.Add(p);
                if (7 <= p.x && p.x <= 9 && 7 <= p.y && p.y <= 9)
                    list009.Add(p);
            }

            //Parts.possible 정리하기
            foreach(List<Part> l in lists)
            {
                List<int> numbers = new List<int>();
                foreach (Part p in l)
                    numbers.Add(p.num);
                List<int> left = getLeft(numbers);
                foreach(Part p in l)
                {
                    if(p.num == 0)
                    {
                        if (p.possible == null)
                        {
                            p.possible = left;
                            logger.write(p, "fisrt  possible : " + p.getPossible());
                        }
                        else
                        {
                            List<int> common = getCommon(p.possible, left);
                            p.possible = common;
                            logger.write(p, "second possible : " + p.getPossible());
                        }
                    }
                }
            }

            //Calculate
            while (true)
            {
                int change = 0;
                onePossible(totalList, logger, ref change);
                if(change == 1)
                    continue;

                alonePossible(totalList, lists, logger, ref change);

                if (change == 0)
                {
                    logger.Complete();
                    Console.WriteLine("NO MORE CHANGES");
                    break;
                }                    
                int Check = 0;
                foreach(Part p in totalList)
                {
                    if (p.num == 0)
                        Check = 1;
                }
                if (Check == 0)
                {
                    logger.write("Completed");
                    logger.Complete();
                    break;
                }
            }

            //가정법 문법 시작
            TryMethod tm = new TryMethod(0,0, totalList, lists, logger);

            //정답 게시
            writeAnswer(lists);
            logger.write(lists);
            logger.Complete();
        }

        //possible이 한 개인 경우
        public static void onePossible(List<Part> totalList, Logger logger, ref int change)
        {
            foreach (Part p in totalList)
            {
                if (p.possible != null)
                {
                    if (p.possible.Count == 1)
                    {
                        p.num = p.possible.ToArray()[0];
                        p.possible = null;
                        logger.write(p, "Match1 : " + p.num);
                        //나머지 possible에서 겹치는 수 제거
                        foreach (Part a in totalList)
                            if (a.possible != null&&((a.x == p.x)||(a.y == p.y)))
                            {
                                a.possible.Remove(p.num);
                                logger.write(a, "Left Possible : " + a.getPossible());
                            }
                            else if(a.possible != null && ((a.x-1)/3 == (p.x-1)/3) && ((a.y-1)/3 == (p.y-1) / 3))
                            {
                                a.possible.Remove(p.num);
                                logger.write(a, "Left Possible : " + a.getPossible());
                            }
                        change = 1;
                    }
                }
            }
        }

        //그 줄에서 가능한 애가 나 혼자인경우
        public static void alonePossible(List<Part> totalList, List<List<Part>> lists, Logger logger, ref int change)
        {
            foreach (List<Part> l in lists) {
                if (alonePossible(l, out Part p, out int m))
                {
                    int x = p.x;
                    int y = p.y;
                    //p 정리
                    p.num = m;
                    p.possible = null;
                    logger.write(p, "확정 : " + p.num);
                    //나머지 줄 정리
                    foreach(Part a in totalList)
                    {
                        if (a.possible == null)
                            continue;
                        if((a.x == p.x)||(a.y == p.y))
                        {
                            a.possible.Remove(p.num);
                            logger.write(a, "possible remove : " + p.num);
                        }
                        else if ((a.x - 1) / 3 == (p.x - 1) / 3 && ((a.y - 1) / 3 == (p.y - 1) / 3))
                        {
                            a.possible.Remove(p.num);
                            logger.write(a, "possible remove : " + p.num);
                        }
                    }
                    change = 1;
                }
                logger.write("no change in : "+"("+l.ToArray()[0].x+","+ l.ToArray()[0].y+")");
            }
        }
        //그 줄에서 그 수를 나만 possible로 가지고 있는 경우
        static bool alonePossible(List<Part> list, out Part p, out int m)
        {
            for(int i = 1; i <= 9; i++)
            {
                int count = 0;
                foreach (Part a in list)
                {
                    if (a.possible == null)
                        continue;
                    foreach(int n in a.possible)
                    {
                        if(n == i)
                        {
                            count++;
                        }
                    }
                }
                if(count == 1)
                {
                    foreach(Part a in list)
                    {
                        if (a.possible == null)
                            continue;
                        foreach(int n in a.possible)
                        {
                            if(n == i)
                            {
                                p = a;
                                m = n;
                                return true;
                            }
                        }
                    }
                }
            }
            p = null;
            m = 10;
            return false;
        }
        static void placeController(ref int n) //readNumbers()의 부속 함수
        {
            if (n % 10 == 9)
                n = n + 2;
            else
                n = n + 1;
        }
        static void readNumbers(List<Part> totalList) //81개 숫자를 모두 읽음, 빈칸은 0 입력.
        {
            int i = 11;
            for (int n = 1; n <= 81; n++)
            {
                Console.Write(i + ": ");
                int input = int.Parse(Console.ReadLine());
                Part part = new Part() { x = i % 10, y = i / 10, num=input };

                totalList.Add(part);
                placeController(ref i);
            }
        }
        //가정법 사용
        static List<Part> tryAll(List<Part> tList)
        {
            int tryCount = 0;
            while (true)
            {
                TryPart tryPart = new TryPart();
                foreach(Part p in tList)
                {
                    tryPart.totalList.Add((Part)p.Clone());
                }

                Part pMin = minPossible(tryPart.totalList);

                int try1 = pMin.possible.ToArray()[tryCount];
                pMin.num = try1;
                pMin.possible = null;



                int x = pMin.x;
                int y = pMin.y;
            }
        }
        //최소 남은 가능성 Part 반환
        static Part minPossible(List<Part> l)
        {
            l.Sort();
            return l.ToArray()[0];
        }

        //틀렸는지 확인
        public static bool isCorrect(List<Part> totalList)
        {
            foreach(Part p in totalList)
            {
                if (p.possible == null)
                    continue;

                if (p.possible.Count == 0)
                    return false;
            }
            return true;
        }
        //시험용 데이터 입력
        static void readNumbers(List<Part> totalList, int m)
        {
            int i = 11;
            int[] numList = null;
            if (m ==3)
                numList = new int[81] { 5,0,0,9,0,0,2,0,0,0,0,0,0,0,7,0,0,0,8,0,0,6,0,0,0,4,0,0,6,0,0,0,0,0,1,0,0,0,0,0,0,0,0,6,7,3,0,4,0,0,1,0,0,9,0,9,0,8,0,0,0,0,0,0,1,0,7,4,5,0,0,0,0,0,3,0,0,0,4,0,0};
            else if( m == 1)
                numList = new int[81] { 9,0,0,8,1,0,0,0,0,0,0,5,0,0,4,7,0,6,0,0,0,2,0,5,8,0,1,0,9,0,7,4,0,5,0,0,0,0,0,0,0,3,0,7,0,7,4,0,0,0,0,0,0,0,3,0,0,9,5,0,6,0,0,0,0,6,4,0,0,0,1,3,1,7,0,0,0,0,0,0,4 };
            else if( m ==2)
                numList = new int[81] { 0,1,0,0,8,0,0,0,0,0,9,6,2,0,7,0,0,0,0,0,8,6,0,0,5,4,0,0,0,0,0,0,0,0,8,0,1,0,0,7,0,5,0,0,6,0,6,0,0,0,0,1,5,7,0,0,3,0,6,1,0,0,0,0,0,0,4,0,0,7,6,3,0,0,0,0,7,0,4,0,0};
            for (int n =0;n <=80; n++)
            {
                Part part = new Part() { x = i % 10, y = i / 10, num = numList[n] };
                totalList.Add(part);
                placeController(ref i);
            }
        }
        static List<int> getCommon(List<int> list1, List<int> list2) //두 리스트에 공통인 숫자들 리스트로 반환
        {
            List<int> result = new List<int>();
            foreach (int n in list1)
            {
                foreach (int m in list2)
                {
                    if (n == m)
                        result.Add(n);
                }
            }
            return result;
        }
        static List<int> getLeft(List<int> list) //리스트에 없는 숫자를 찾아냄
        {
            List<int> left = new List<int>();
            for (int n = 1; n <= 9; n++)
            {
                if (numCheck(n, list))
                    left.Add(n);
            }
            return left;
        }
        static bool numCheck(int n, List<int> list) //리스트에 들어갈 수 있는 숫자인 경우 true반환
        {
            foreach (int m in list)
            {
                if (n == m)
                    return false;
            }
            return true;
        }

        static void writeAnswer(List<List<Part>> lists)
        {
            int i = 1;
            foreach (List<Part> l in lists)
            {
                if (i <= 9 || i>18)
                {
                    i++;
                    continue;
                }
                i++;
                string text = "";
                foreach (Part p in l)
                {
                    text += (p.num.ToString() + " ");
                }
                Console.WriteLine(text);
            }
        }
    }

    class Part : IComparable<Part>, ICloneable
    {
        public int num { get; set; }
        public List<int> possible = null;
        public string getPossible()
        {
            string msg = "";
            foreach (int n in possible)
                msg += (n + " ");
            return msg;
        }
        public int x { get; set; }
        public int y { get; set; }

        public int CompareTo(Part comparePart)
        {
            // A null value means that this object is greater.
            if (comparePart.possible.Count >= this.possible.Count)
                return 1;

            else
                return 0;
        }
        public object Clone()
        {
            Part p = new Part();
            p.num = this.num;
            p.possible = this.possible;
            p.x = this.x;
            p.y = this.y;

            return p;
        }
    }
    
    class Logger
    {
        StreamWriter sw = new StreamWriter(new FileStream("./log.txt", FileMode.Create, FileAccess.ReadWrite));
        public void write(string text)
        {
            DateTime now = DateTime.Now;
            string msg = now+ " : " +text;
            sw.Write("------"+msg+"------\n");
        }
        public void write(Part part, string text)
        {
            DateTime now = DateTime.Now;
            String place = "(" + part.x + "," + part.y + ")";
            string msg = now + " : " + place + " " + text+"\n";
            sw.Write(msg);
        }
        public void write(List<List<Part>> lists)
        {
            sw.Write("THE ANSWER"+"\n");
            int i = 1;
            foreach (List<Part> l in lists)
            {
                if (i <= 9 || i > 18)
                {
                    i++;
                    continue;
                }
                i++;
                string text = "";
                foreach (Part p in l)
                {
                    text += (p.num.ToString() + " ");
                }
                sw.Write(text+"\n");
            }
        }
        public void Enter()
        {
            sw.Write("\n");
        }
        public void Complete()
        {
            sw.Flush();
            Console.WriteLine("Flush COMPLETED");
        }
    }

    class TryPart
    {
        public List<Part> totalList = new List<Part>();
    }
    class TryMethod
    {
        int count;
        int tryCount;
        Part p; //이번에 가정할 Part
        Logger logger;
        List<Part> totalList;
        List<List<Part>> lists;
        public TryMethod(int count, int tryCount, List<Part> totalList, List<List<Part>> lists, Logger logger)
        {
            this.count = count;
            this.tryCount = tryCount;
            this.totalList = totalList;

            Part[] tList = totalList.ToArray();
            List<Part> emptyList = new List<Part>();
            foreach(Part p in tList)
            {
                if (p.possible == null)
                    continue;
                emptyList.Add(p);
            }
            p = emptyList.ToArray()[count];
            this.logger = logger;
            this.lists = lists;
            if (runWhile(tryCount))
            {
                TryMethod tm = new TryMethod(count, tryCount+1,  totalList, lists, logger);
            }
        }
        bool runWhile(int tryCount)
        {
            List<int> tempPossible = new List<int>();
            int possibleCount = p.possible.Count;
            while (true)
            {
                foreach(int n in p.possible.ToList())
                {
                    int m = n;
                    tempPossible.Add(m);
                }
                if (tryCount > possibleCount-2)
                {
                    return false;
                }
                p.num = p.possible.ToArray()[tryCount];
                p.possible = null;

                logger.write(p, "try : " + p.num);
                //나머지 possible에서 겹치는 수 제거
                foreach (Part a in totalList)
                    if (a.possible != null && ((a.x == p.x) || (a.y == p.y)))
                    {
                        a.possible.Remove(p.num);
                        logger.write(a, "Left Possible : " + a.getPossible());
                    }
                    else if (a.possible != null && ((a.x - 1) / 3 == (p.x - 1) / 3) && ((a.y - 1) / 3 == (p.y - 1) / 3))
                    {
                        a.possible.Remove(p.num);
                        logger.write(a, "Left Possible : " + a.getPossible());
                    }
                //---가정 반영구역 ------ ^^^^^^

                
                while (true)
                {
                    int change = 0;
                    Program.onePossible(totalList, logger, ref change);
                    if (change == 1)
                        continue;

                    Program.alonePossible(totalList, lists, logger, ref change);
                    int Check = 0;
                    if (change == 0)
                    {
                        logger.Complete();

                        Console.WriteLine("!!");
                        foreach (Part p in totalList)
                        {
                            if (p.num == 0)
                                Check = 1;
                        }
                        if (Check == 0)   //계산 완료!!!!
                        {
                            logger.write("Completed");
                            logger.write(lists);
                            break;
                        }

                        if (Program.isCorrect(totalList))
                        {
                            logger.write("가정 하나 더 해야 함.");
                            Console.WriteLine("!!");
                            logger.Complete();
                            //하나 더 가정해야 함.
                            TryMethod tm = new TryMethod(count+1,0, totalList, lists, logger);   //count +1 : 몇번째 수를 가정하는 것인지에 대한 것임.
                            break;
                        }
                        else
                        {
                            logger.write("가정 다른 것으로 해야 함.");
                            logger.Complete();
                            foreach(int n in tempPossible)
                            {
                                p.possible = new List<int>();
                                int m = n;
                                p.possible.Add(m);
                            }
                            //다른 것으로 가정해야 함.
                            tryCount++;
                            break;
                        }
                    }
                    
                    
                }
                
            }
        }
    }
}
