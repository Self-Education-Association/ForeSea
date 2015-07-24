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
        public Student(object id,object name,object state,object room)
        {
            this.id = int.Parse(id.ToString());
            this.name = name.ToString();
            this.state = int.Parse(state.ToString());
            this.room = room.ToString();
        }
        public int id;
        public string name;
        public int state;
        public string room;
    }
}
