
namespace DatabaseManagement
{
    internal static class Script
    {
        internal static string IsCreated(bool isCreated)
        {
            if (isCreated)
            {
                return "База данных была создана";
            }
            else
            {
                return "База данных уже существует";
            }
        }

        internal static string IsDelete(bool isCreated)
        {
            if (isCreated)
            {
                return "База данных была удалена";
            }
            else
            {
                return "База данных уже удалена";
            }
        }

    }
}
