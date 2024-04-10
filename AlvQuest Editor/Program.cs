namespace AlvQuest_Editor
{
    internal static class Program
    {
        public static CharacterCard CharacterCard { get; set; } = new CharacterCard();
        public static MainMenu MainMenu { get; set; } = new MainMenu(CharacterCard);

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CharacterCard.Show();
            Application.Run(MainMenu);
        }
    }
}
