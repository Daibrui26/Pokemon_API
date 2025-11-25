using Models;

public class Estadistica {
    
    public int HP { get; set; }
    public int ATk { get; set; }
    public int DEF { get; set; }
    public int SATK { get; set; }
    public int SDEF { get; set; }
    public int SPE { get; set; }

    public Estadistica(int hp, int atk, int def, int satk, int sdef, int spe)
    {
        HP = hp;
        ATk = atk;
        DEF = def;
        SATK = satk;
        SDEF = sdef;
        SPE = spe;
    }   

    public Estadistica(){}

}
