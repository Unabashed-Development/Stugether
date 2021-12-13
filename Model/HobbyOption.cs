namespace Model
{
    /// <summary>
    /// Holds data for binded hobby, Id: hobby ID, Name: hobby name, IsChecked: returns if the hobby is selected in the profile settings
    /// </summary>
    public class HobbyOption
    {

        #region properties
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }
        #endregion
    }
}
