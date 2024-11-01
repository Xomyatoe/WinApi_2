using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Clock
{
    public class Alarm: IComparable
    {
        public static readonly string[]  WeekDayNames= new string[7]{"Пн","Вт","Ср","Чт","Пт","Сб","Вс"};
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public bool[] Weekdays { get; private set; }
        public string Filename { get; set; } = "";
        public Alarm()
        { 
        Weekdays = new bool[7];
        }
        //public Alarm(Alarm other):this()
        //{
        //    this.Date = other.Date;
        //    this.Time = other.Time;
        //    this.Filename = other.Filename;
        //    for(int i = 0; i < Weekdays.Length; i++) this.Weekdays[i] = other.Weekdays[i];
        //}
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

    public int CompareTo(object other)
    {
        return this.Time.CompareTo((other as Alarm).Time); 
    }//оператов as преобразует значение слева в тип справа
    }
}
