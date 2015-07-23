using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckInProgram
{
    public class Student
    {
        public Student(int id,string name,int state,string room)
        {
            this.id = id;
            this.name = name;
            this.state = state;
            this.room = room;
        }
        public int id;
        public string name;
        public int state;
        public string room;
    }
}
