using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductivityUnit : Unit
{
    private ResourcePile m_CurrentPile = null;
    public float ProductivityMultiplier = 2;


    protected override void BuildingInRange() // must override this inherited "Unit" method
    {   
        if (m_CurrentPile == null)  {
            // if target is "ResourcePile" type, pile is assigned, else it is null
            ResourcePile pile = m_Target as ResourcePile; 

            // if the target was assigned
            if(pile != null) {
                m_CurrentPile = pile;
                m_CurrentPile.ProductionSpeed *= ProductivityMultiplier;
            }
        }
    }


    void ResetProductivity() {
        if (m_CurrentPile != null) {
            m_CurrentPile.ProductionSpeed /= ProductivityMultiplier;
            m_CurrentPile = null;
        }
    }

    public override void GoTo(Building target)
    {
        ResetProductivity();
        base.GoTo(target);
    }

    
    public override void GoTo(Vector3 position)
    {
        ResetProductivity();
        base.GoTo(position);
    }
}
