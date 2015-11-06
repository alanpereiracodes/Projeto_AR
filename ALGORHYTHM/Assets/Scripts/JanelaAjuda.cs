using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JanelaAjuda : MonoBehaviour {

	//public Text textoObjetivo; //Texto sobre o Objetivo da Fase Atual;
	//public Sprite spriteFase; //Imagem da Fase Atual, para compor a tela sobre o Objetivo na Janela de Ajuda

	public Scrollbar rolagemAjuda; //O valor aqui ira determinar a Seçao que esta // 1 Objetivo ... 3 Como Jogar : Executando ... 9 Objeto pt2. 

	private float x;

	public void Avanca()
	{
		x = rolagemAjuda.value;
		if(Mathf.Abs(x) < 0.11f)
		{
			Canvas.ForceUpdateCanvases();
			rolagemAjuda.value = 0.11f;
		}
		else
		{
			if(0.11f <= Mathf.Abs(x) && Mathf.Abs(x) < 0.23f)
			{
				Canvas.ForceUpdateCanvases();
				rolagemAjuda.value = 0.23f;
				Canvas.ForceUpdateCanvases();
				rolagemAjuda.value = 0.23f;
			}
			else
			{
				if(0.23f <= Mathf.Abs(x) && Mathf.Abs(x) < 0.34f)
				{
					Canvas.ForceUpdateCanvases();
					rolagemAjuda.value = 0.34f;
					Canvas.ForceUpdateCanvases();
					rolagemAjuda.value = 0.34f;
				}
				else
				{
					if(0.34f <= Mathf.Abs(x) && Mathf.Abs(x) < 0.45f)
					{
						Canvas.ForceUpdateCanvases();
						rolagemAjuda.value = 0.45f;
						Canvas.ForceUpdateCanvases();
						rolagemAjuda.value = 0.45f;
					}
					else
					{
						if(0.45f <= Mathf.Abs(x) && Mathf.Abs(x) < 0.56f)
						{
							Canvas.ForceUpdateCanvases();
							rolagemAjuda.value = 0.56f;
							Canvas.ForceUpdateCanvases();
							rolagemAjuda.value = 0.56f;
						}
						else
						{
							if(0.56f <= Mathf.Abs(x) && Mathf.Abs(x) < 0.67f)
							{
								Canvas.ForceUpdateCanvases();
								rolagemAjuda.value = 0.67f;
								Canvas.ForceUpdateCanvases();
								rolagemAjuda.value = 0.67f;
							}
							else
							{	
								if(0.67f <= Mathf.Abs(x) && Mathf.Abs(x) < 0.79f)
								{
									Canvas.ForceUpdateCanvases();
									rolagemAjuda.value = 0.79f;
									Canvas.ForceUpdateCanvases();
									rolagemAjuda.value = 0.79f;
								}
								else
								{
									if(0.79f <= Mathf.Abs(x) && Mathf.Abs(x) < 0.89f)
									{
										Canvas.ForceUpdateCanvases();
										rolagemAjuda.value = 0.89f;
										Canvas.ForceUpdateCanvases();
										rolagemAjuda.value = 0.89f;
									}
									else
									{
										if(0.89f <= x)
										{
											Canvas.ForceUpdateCanvases();
											rolagemAjuda.value = 1f;
											Canvas.ForceUpdateCanvases();
											rolagemAjuda.value = 1f;
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}

	public void Retorna()
	{
		x = rolagemAjuda.value;
		if(0.889f < x)
		{
			Canvas.ForceUpdateCanvases();
			rolagemAjuda.value = 0.889f;
		}
		else
		{
			if(0.79f < x && x <= 0.889f)
			{
				Canvas.ForceUpdateCanvases();
				rolagemAjuda.value = 0.79f;
			}
			else
			{
				if(0.675f < x && x <= 0.79f)
				{
					Canvas.ForceUpdateCanvases();
					rolagemAjuda.value = 0.675f;
				}
				else
				{
					if(0.565f < x && x <= 0.675f)
					{
						Canvas.ForceUpdateCanvases();
						rolagemAjuda.value = 0.565f;
					}
					else
					{
						if(0.456f < x && x <= 0.565f)
						{
							Canvas.ForceUpdateCanvases();
							rolagemAjuda.value = 0.456f;
						}
						else
						{
							if(0.34f < x && x <= 0.456f)
							{
								Canvas.ForceUpdateCanvases();
								rolagemAjuda.value = 0.34f;
							}
							else
							{
								if(0.23f < x && x <= 0.34f)
								{
									Canvas.ForceUpdateCanvases();
									rolagemAjuda.value = 0.23f;
								}
								else
								{
									if(0.11f < x && x <= 0.23f)
									{
										Canvas.ForceUpdateCanvases();
										rolagemAjuda.value = 0.11f;
									}
									else
									{
										if(0.11f >= x)
										{
											Canvas.ForceUpdateCanvases();
											rolagemAjuda.value = 0f;
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}

	public void FechaAjuda()
	{
		Canvas.ForceUpdateCanvases();
		rolagemAjuda.value = 0.0f;
		Canvas.ForceUpdateCanvases();
		this.gameObject.SetActive(false);
		Debug.Log(rolagemAjuda.value);

	}

	public void AbreAjuda()
	{
		this.gameObject.SetActive(true);
		Canvas.ForceUpdateCanvases();
		rolagemAjuda.value = 0.0f;
		Canvas.ForceUpdateCanvases();
		Debug.Log(rolagemAjuda.value);
	}
}
