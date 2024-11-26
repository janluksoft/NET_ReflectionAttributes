//==== Author: janluksoft@interia.pl ==========================

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace UserAttributes
{

    //======== Demonstration of reflection and user attributes =========

    class Program
    {
        static void Main(string[] args)
        {
            //We get attributes for the class [MyClassMarkedWithAttributes]
            System.Reflection.MemberInfo info = typeof(MyClassMarkedWithAttributes);
            //Oblong rec = new Oblong(5.1, 17.2);
            Type typeObl = typeof(Oblong);


            var ListType = new List<System.Reflection.MemberInfo>();
            ListType.Add(typeof(MyClassMarkedWithAttributes));
            ListType.Add(typeof(Oblong));

            Console.WriteLine("Demonstration of reflection and user attributes in .NET8");

            //Attributes in Types
            Console.WriteLine("\n\rAttributes in Types:");
            foreach (MemberInfo atype in ListType) // attributes)
                PrintAttributes(atype.GetCustomAttributes(true));

            //Attributes in methods
            Console.WriteLine("\n\rAttributes in methods:");
            foreach (Type atype in ListType) // attributes)
                foreach (MethodInfo method in atype.GetMethods())
                    //foreach (Attribute a in method.GetCustomAttributes(true))
                    PrintAttributes(method.GetCustomAttributes(true));

            Console.WriteLine("");
            //Console.ReadKey();
        } // --- End Main(string[] args) -------------


        static void PrintAttributes(object[] xListObj)
        {
            foreach (Object item in xListObj) // attributes
            {
                //We show all attributes
                Console.WriteLine("  " + item);

                switch (item) //Pattern Matching C#7
                {
                    case FirstMyAttribute fAttrib:
                        // Attribute description
                        //FirstMyAttribute ea = (FirstMyAttribute)attributes[i];
                        //typeof attributes[i];
                        Console.WriteLine("    Info: FirstMyAttribute: \"{0}\", \"{1}\"", fAttrib.message, fAttrib.Topic);
                        break;

                    case SecondMyAttribute sAttrib:
                        Console.WriteLine("    Info: SecondMyAttribute: \"{0}\"", sAttrib.message);

                        break;
                    case AttrWorkProp sAtt:
                        string s = $"code:{sAtt.Id}; name:{sAtt.Name}; date:{sAtt.Jdata}";
                        Console.WriteLine($"    Info: AttrWorkProp: mess:\"{sAtt.message}\" " + s);
                        break;
                    case ObsoleteAttribute sAtt:
                        string s2 = $"IsError:{sAtt.IsError}; Id:{sAtt.DiagnosticId};";
                        Console.WriteLine($"    Info: Obsolete: mess:\"{sAtt.Message}\" " + s2);
                        break;

                        //case Rectangle r when r.Height > 20:
                        //    st = "Rectangle big"; break;
                }
            }
        }

        // The result of the program:
        //   CSharpFeatures.SecondMyAttribute
        //     Info SecondMyAttribute: "Class is empty for now"
        //   CSharpFeatures.FirstMyAttribute
        //     Info FirstMyAttribute: "It is my class", "Topic: swimming"

    }

  #region --- Attributes define -------------

    //user attribute class is created
    [AttributeUsage(AttributeTargets.All)]
    public class FirstMyAttribute : Attribute //Attribute abstract
    {
        public readonly string message;
        private string topic;
        public FirstMyAttribute(string xMessage, string xtopic)
        {
            this.message = xMessage;
            this.Topic = xtopic;
        }
        public string Topic { get {return topic;} set {topic = value;} }
    }

    [AttributeUsage(AttributeTargets.All)]
    public class SecondMyAttribute : Attribute //Attribute abstract
    {
        public readonly string message;
        public SecondMyAttribute(string xMessage)
        {
            this.message = xMessage;
        }
    }


    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class |
        AttributeTargets.Constructor | AttributeTargets.Field |
        AttributeTargets.Property, AllowMultiple = true)]
    public class AttrWorkProp : Attribute
    {
        // pola prywatne
        private int id;
        private string name;
        private string jdata;

        public string message;
        public AttrWorkProp(int id, string jdata, string name)
        {
            this.id = id;
            this.jdata = jdata;
            this.name = name;
        }

        //C# 11 declarations
        public int Id { get => id; } //Read only
        public string Name { get => name; }
        public string Jdata { get => jdata; }
    }

  #endregion Attributes define
  
  #region --- Class with Attributes -------------

    [SecondMyAttribute("Class is empty for now")]
    [FirstMyAttribute("It is my class", "Topic: swimming")]
    class MyClassMarkedWithAttributes
    {
        //predefined attributes for metod
        [Conditional("DEBUG")]
        [Obsolete("Don't use this version - it is obsolete", false)] //true Will report a compilation error
        [FirstMyAttribute("This method is obsolete.", "Topic: Method:DebugMessage")]
        public static void DebugMessage(string message)
        {
            Console.WriteLine(message);
        }
    }


    [AttrWorkProp(401, "2022-12-21", "Mads Pedersen", message = "Wrong type")]
    [AttrWorkProp(506, "2022-10-23", "Peter", message = "Overflow memory")]
    class Oblong
    {
        protected double length;
        protected double width;
        public Oblong(double l, double w)
        {
            length = l;
            width = w;
        }
        
        [AttrWorkProp(102, "2022-10-06", "Greg", message = "Method done well")]
        public double GetWholeAreaM2()
        {
            return(length * width);
        }
    }

    #endregion --- Class with Attributes -------------

}
