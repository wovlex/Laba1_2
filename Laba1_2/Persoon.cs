using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Laba1_2
{
    public class Person
    {

        
        
            [JsonInclude] //Атрибут будет сохранен в json
            int age;
            [JsonInclude]
            string first_name;
            [JsonInclude]
            string second_name;
            [JsonInclude]
            double height;

            public Person(int Age, string FName, string SName, double Height)
            {
                age = Age;
                first_name = FName;
                second_name = SName;
                height = Height;
            }

            public int getAge() { return age; }
            public string getFirstName() { return first_name; }
            public string getSecondName() { return second_name; }
            public double getHeight() { return height; }
        
    }
    
}
