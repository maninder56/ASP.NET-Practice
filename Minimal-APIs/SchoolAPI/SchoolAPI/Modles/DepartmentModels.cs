namespace SchoolAPI.Modles.DepartmentModels; 

public class DepartmentModel
{
    public int DepartmentId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Budget { get; set; }

    public DateTime StartDate { get; set; }

    public int? Administrator { get; set; }
}


//public class DepartmentModelWihtCources
//{
//    public int DepartmentId { get; set; }

//    public string Name { get; set; } = null!;

//    public decimal Budget { get; set; }

//    public DateTime StartDate { get; set; }

//    public int? Administrator { get; set; }
//}

