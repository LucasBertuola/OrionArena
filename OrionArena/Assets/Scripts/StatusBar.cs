using UnityEngine;
using System.Collections;

public class StatusBar : MonoBehaviour
{
	public float PegarTamanhoBarra(float _minValor, float maxValor)
	{
		return _minValor / maxValor;
	}

	public int PegarPorcentageBarra(float _minValor, float maxValor)
	{
		return Mathf.RoundToInt(PegarTamanhoBarra(_minValor, maxValor) * 100);
	}

}
