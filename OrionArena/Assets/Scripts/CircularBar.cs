using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CircularBar : MonoBehaviour
{

	public StatusBar _statusBar;
	public Image _bar;
	public Text _texto;
	float _maxValor;
	float _minValor;

	public Ability ability;

	// Use this for initialization
	void Start()
	{
		_statusBar = this.gameObject.GetComponent<StatusBar>();
		_maxValor = ability.timeForAbility;
	}

	// Update is called once per frame
	void Update()
	{
		_bar.fillAmount = 1 - _statusBar.PegarTamanhoBarra(_minValor, _maxValor);
		_texto.text =  (_maxValor - (_statusBar.PegarPorcentageBarra(_minValor, _maxValor) * (_maxValor * 0.01f))).ToString("N1");

		if (_minValor < _maxValor)
		{
			_minValor = ability.timeAt;
		}
		else
		{
			_texto.enabled = false;
		}

	}

	public void ResetAbility()
	{
		_minValor = 0;
		_texto.enabled = true;
	}
}
