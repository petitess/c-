namespace exercise01
{
    internal class FileName
    {
        static void Main(string[] args)
        {

            //We simply apply our Student-Structure to XML. 
            string studentsXML =
                        @"<Students>
                            <Student>
                                <Name>Toni</Name>
                                <Age>21</Age>
                                <University>Yale</University>
                                <Semester>6</Semester>
                            </Student>
                            <Student>
                                <Name>Carla</Name>
                                <Age>17</Age>
                                <University>Yale</University>
                                <Semester>1</Semester>
                            </Student>
                            <Student>
                                <Name>Leyla</Name>
                                <Age>19</Age>
                                <University>Beijing Tech</University>
                                <Semester>3</Semester>
                            </Student>
                            <Student>
                                <Name>Frank</Name>
                                <Age>25</Age>
                                <University>Beijing Tech</University>
                                <Semester>10</Semester>
                            </Student>
                        </Students>";
            XDocument studentsXdco = new XDocument();
            studentsXdco = XDocument.Parse(studentsXML);
            var students = from student in studentsXdco.Descendants("Student")
                           select new
                           {
                               Name = student.Element("Name").Value,
                               Age = student.Element("Age").Value,
                               University = student.Element("University").Value,
                               Semester = student.Element("Semester")
                           };
            foreach (var student in students)
            {
                Console.WriteLine("Studen {0} with age {1} from university {2}", student.Name, student.Age, student.University);
            }

            var sortedStudents = from student in students orderby student.Age select student;
            foreach (var student in sortedStudents)
            {
                Console.WriteLine("Studen {0} with age {1} from university {2}", student.Name, student.Age, student.University);
            }
            Console.ReadLine();
        }
    }
}
