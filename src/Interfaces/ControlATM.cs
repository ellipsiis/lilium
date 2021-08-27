namespace lilium.src.interfaces
{
    interface ControlATM
    {
        void LoginUser();
        bool CheckCard(string name, string lastname, string card);
    }
}
