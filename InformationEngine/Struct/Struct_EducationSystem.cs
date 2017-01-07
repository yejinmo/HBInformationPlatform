using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace InformationEngine.Struct
{
    [Serializable]
    public class Struct_EducationSystem : Struct_Root
    {

        public Data_Info Data = new Data_Info();

        public class Data_Info
        {
            public List<Course_Info> CourseSchedule = new List<Course_Info>();
            public List<ExaminationQuery_Info> ExaminationQuery = new List<ExaminationQuery_Info>();
            public List<Score_Info> Score = new List<Score_Info>();
        }

        public class Course_Info
        {
            /// <summary>
            /// 课程名称
            /// </summary>
            public string Course_Name = string.Empty;
            /// <summary>
            /// 星期几
            /// </summary>
            public string Day_Of_Week = string.Empty;
            /// <summary>
            /// 第几节课（如 1，2 节）
            /// </summary>
            public string Number_Of_Day = string.Empty;
            /// <summary>
            /// 开始周
            /// </summary>
            public string Begin_Week = string.Empty;
            /// <summary>
            /// 结束周
            /// </summary>
            public string End_Week = string.Empty;
            /// <summary>
            /// 教师名
            /// </summary>
            public string Teacher_Name = string.Empty;
            /// <summary>
            /// 上课地点
            /// </summary>
            public string Address = string.Empty;
        }

        public class Score_Info
        {
            /// <summary>
            /// 学年
            /// </summary>
            public string School_Year;
            /// <summary>
            /// 学期
            /// </summary>
            public string Semester;
            /// <summary>
            /// 课程名
            /// </summary>
            public string Name;
            /// <summary>
            /// 课程性质
            /// </summary>
            public string Course_Type;
            /// <summary>
            /// 学分
            /// </summary>
            public string Credit;
            /// <summary>
            /// 绩点
            /// </summary>
            public string Grade_Point;
            /// <summary>
            /// 成绩
            /// </summary>
            public string Result;
            /// <summary>
            /// 补考成绩
            /// </summary>
            public string BK_Result;
            /// <summary>
            /// 重修成绩
            /// </summary>
            public string CX_Result;
            /// <summary>
            /// 开课学院
            /// </summary>
            public string Department;
            /// <summary>
            /// 备注
            /// </summary>
            public string Remark;
        }

        public class ExaminationQuery_Info
        {
            /// <summary>
            /// 课程名称
            /// </summary>
            public string Course_Name = string.Empty;
            /// <summary>
            /// 考试地点
            /// </summary>
            public string Address = string.Empty;
            /// <summary>
            /// 考试时间
            /// </summary>
            public string Date = string.Empty;
            /// <summary>
            /// 座位号
            /// </summary>
            public string Number = string.Empty;
        }

        public static string Serialiaze(Struct_EducationSystem obj)
        {
            return (new JavaScriptSerializer().Serialize(obj));
        }

        public static Struct_EducationSystem Deserialize(string json)
        {
           return new JavaScriptSerializer().Deserialize<Struct_EducationSystem>(json);
        }

    }
}
