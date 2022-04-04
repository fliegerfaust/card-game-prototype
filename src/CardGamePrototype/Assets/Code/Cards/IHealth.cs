namespace Code.Cards
{
  public interface IHealth
  {
    void TakeHeal(int value);
    void TakeDamage(int value);
    int Current { get; set; }
  }
}