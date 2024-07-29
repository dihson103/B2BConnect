namespace Contract.Services.Business.Share;
public static class NumberOfEmployeeExtension
{
    private static readonly Dictionary<NumberOfEmployee, string> _noeDescriptions = new()
    {
        {NumberOfEmployee.FROM_0_TO_50 , "Dưới 50 nhân viên" },
        {NumberOfEmployee.FROM_50_TO_100 , "Từ 50 đến 100 nhân viên" },
        {NumberOfEmployee.FROM_100_TO_200 , "Từ 100 đến 200 nhân viên" },
        {NumberOfEmployee.FROM_200_TO_500 , "Từ 200 đến 500 nhân viên" },
        {NumberOfEmployee.FROM_500_TO_1000 , "Từ 500 đến 1000 nhân viên" },
        {NumberOfEmployee.OVER_1000 , "Trên 1000 nhân viên" }
    };

    public static string GetDescription(this NumberOfEmployee status)
    {
        return _noeDescriptions[status];
    }
}
