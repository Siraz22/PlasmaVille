using UnityEngine;

public class SelectWBC : MonoBehaviour
{
    public WBCBlueprint Neutrophil;
    public WBCBlueprint KillerTCell;

    Singletons buildmanager;
    private void Start()
    {
        buildmanager = Singletons.Instance;
    }

    public void SelectKillerTCell()
    {
        buildmanager.SelectTurretToBuild(KillerTCell);
        Debug.Log("Killer T Cell");
    }

    public void SelectNeutrophil()
    {
        buildmanager.SelectTurretToBuild(Neutrophil);

        Debug.Log("neutrophil");
    }
}
