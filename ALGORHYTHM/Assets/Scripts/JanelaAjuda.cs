using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JanelaAjuda : MonoBehaviour {

	public Text textoObjetivo; //Texto sobre o Objetivo da Fase Atual;
	public Sprite spriteFase; //Imagem da Fase Atual, para compor a tela sobre o Objetivo na Janela de Ajuda

	public Scrollbar rolagemAjuda; //O valor aqui ira determinar a Seçao que esta // 1 Objetivo ... 3 Como Jogar : Executando ... 9 Objeto pt2. 

	private float x;

	public void Avanca()
	{
		x = rolagemAjuda.value;
		if(x < 0.11f)
		{
			rolagemAjuda.value = 0.11f;
		}
		else
		{
			if(0.11f <= x && x < 0.23f)
			{
				rolagemAjuda.value = 0.23f;
			}
			else
			{
				if(0.23f <= x && x < 0.34f)
				{
					rolagemAjuda.value = 0.34f;
				}
				else
				{
					if(0.34f <= x && x < 0.456f)
					{
						rolagemAjuda.value = 0.456f;
					}
					else
					{
						if(0.456f <= x && x < 0.565f)
						{
							rolagemAjuda.value = 0.565f;
						}
						else
						{
							if(0.565f <= x && x < 0.675f)
							{
								rolagemAjuda.value = 0.675f;
							}
							else
							{	
								if(0.675f <= x && x < 0.79f)
								{
									rolagemAjuda.value = 0.79f;
								}
								else
								{
									if(0.79f <= x && x < 0.889f)
									{
										rolagemAjuda.value = 0.889f;
									}
									else
									{
										if(0.889f <= x)
										{
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
			rolagemAjuda.value = 0.889f;
		}
		else
		{
			if(0.79f < x && x <= 0.889f)
			{
				rolagemAjuda.value = 0.79f;
			}
			else
			{
				if(0.675f < x && x <= 0.79f)
				{
					rolagemAjuda.value = 0.675f;
				}
				else
				{
					if(0.565f < x && x <= 0.675f)
					{
						rolagemAjuda.value = 0.565f;
					}
					else
					{
						if(0.456f < x && x <= 0.565f)
						{
							rolagemAjuda.value = 0.456f;
						}
						else
						{
							if(0.34f < x && x <= 0.456f)
							{
								rolagemAjuda.value = 0.34f;
							}
							else
							{
								if(0.23f < x && x <= 0.34f)
								{
									rolagemAjuda.value = 0.23f;
								}
								else
								{
									if(0.11f < x && x <= 0.23f)
									{
										rolagemAjuda.value = 0.11f;
									}
									else
									{
										if(0.11f >= x)
										{
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
		this.gameObject.SetActive(false);
	}

	public void AbreAjuda()
	{
		this.gameObject.SetActive(true);
		rolagemAjuda.value = 0f;
	}
}
