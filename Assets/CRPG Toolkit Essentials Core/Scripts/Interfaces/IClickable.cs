using UnityEngine;

public interface IClickable
{
    
    //Standard click behavior
    public void OnClick(PartyController party);

    //Shift click behavior, enable multiselect for units
    public void OnShiftClick(PartyController party);


    public void OnHoverStart();

    public void OnHoverEnd();


}
