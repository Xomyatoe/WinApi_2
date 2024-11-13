using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Clock
{
    public class Alarm: IComparable
    {
        public static readonly string[]  WeekDayNames= new string[7]{"Пн","Вт","Ср","Чт","Пт","Сб","Вс"};
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public bool[] Weekdays { get; private set; }
        string filename;
        public string Filename 
        {
            set=>filename=value; 
            get=>File.Exists(filename)? filename: Path.GetFullPath(DEFAULT_ALARM_FILE); 
        }
        static readonly string DEFAULT_ALARM_FILE = "..\\Sound\\gepard-murlyikaet-31139.mp3";
        public Alarm()
        { 
        Weekdays = new bool[7];
        }
        public Alarm(string alarm_string)
        {
            string[] values= alarm_string.Split(',');
            Date = new DateTime(Convert.ToInt64(values[0]));
            Time = new DateTime(Convert.ToInt64(values[1]));
            Weekdays = WeekDaysFromString(values[2]);
            Filename = values[3];
        }

        //public Alarm(Alarm other):this()
        //{
        //    this.Date = other.Date;
        //    this.Time = other.Time;
        //    this.Filename = other.Filename;
        //    for(int i = 0; i < Weekdays.Length; i++) this.Weekdays[i] = other.Weekdays[i];
        //}
        bool[] WeekDaysFromString(string week_string)
        {
            bool[] weekdays = new bool[7];
            if (week_string.Contains("Пн")) weekdays[0]= true;
            if (week_string.Contains("Вт")) weekdays[1]= true;
            if (week_string.Contains("Ср")) weekdays[2]= true;
            if (week_string.Contains("Чт")) weekdays[3]= true;
            if (week_string.Contains("Пт")) weekdays[4]= true;
            if (week_string.Contains("Сб")) weekdays[5]= true;
            if (week_string.Contains("Вс")) weekdays[6]= true;
            return weekdays;
        }
        string WeekDaysToString()
        {
            string days = "";
   
                for(int i=0;i<Weekdays.Length;i++)
            {
                if (Weekdays[i]) days += WeekDayNames[i];
                //Console.WriteLine(Weekdays[i]+"\t");
            }
                return days;
        }

        public override string ToString()
        {
            string result = "";
            if (Date != null&&Date!=DateTime.MinValue) result += $"{Date},";
            result+=$"{Time.TimeOfDay}, {WeekDaysToString()}, {Filename.Split('\\').Last()}";
            return result;
        }
        public string ToFileString()
        {
            string result = "";
            result += $"{Date.Ticks},";
            result += $"{Time.Ticks},";
            result += $"{WeekDaysToString()},";
            result += $"{Filename},";
            return result;
        }
    public int CompareTo(object other)
    {
        return this.Time.TimeOfDay.CompareTo((other as Alarm).Time.TimeOfDay); 
    }//оператов as преобразует значение слева в тип справа
    }
}
