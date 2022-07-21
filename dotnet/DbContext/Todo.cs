namespace TodoApi.App.DbContext;

public class Todo
{
    public int Id { get; set; }
    public string Value { get; set; }
    public bool Checked { get; set; }
}