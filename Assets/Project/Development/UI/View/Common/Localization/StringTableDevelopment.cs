using Project.Runtime.OutGame.Model;

namespace Project.Development.OutGame
{
    public static class StringTableDevelopment
    {
        public static string GetEntry(long id)
        {
            return StringTableModel.GetEntry(id);
        }

        public static void CacheTable()
        {
            StringTableModel.CacheTable();
        }
    }
}