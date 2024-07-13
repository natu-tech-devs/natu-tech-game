
using System.Collections;

public interface ICharacter {
    public float getHealth();
    public CharStates getState();
    public CharSide getSide();
    public void getAttacked(Attack attack);
    public void attack(Attack attack);

    public ElemetType GetElemetType();

}