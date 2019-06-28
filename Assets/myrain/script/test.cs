using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    ParticleSystem.EmissionModule mEmObjRain;　//雨のparticleを扱う変数
    ParticleSystem.EmissionModule mEmObjFog; //霧のパーティクルを扱う変数
    ParticleSystem.EmissionModule mEmObjLightning; //雷のパーティクルを扱う変数
    ParticleSystem ParticleObjRain;
    ParticleSystem ParticleObjFog;
    ParticleSystem ParticleObjLightning;
    private AudioSource audioSource;
    float m_MySliderValue;
    //public AudioClip audioClip1;
    //public AudioClip audioClip2;
    //public AudioClip audioClip3;
    //public AudioClip audioClip4;
    //public AudioClip audioClip5;
    private AudioSource[] sources;
    private AudioSource rain1;
    private AudioSource rain2;
    private AudioSource rain3;
    private AudioSource rain4;
    private AudioSource rain5;
    private AudioSource lightning;
    public int Count;

    void Start()
    {
        //↓Cube の 子オブジェクトである"Rain"オブジェクトから ParticleSystemコンポーネントを取得 
        ParticleObjRain = transform.FindChild("Rain").GetComponent<ParticleSystem>();
        //↓最終目的である rate にアクセスするために必要な emission を取得し格納
        mEmObjRain = ParticleObjRain.emission;

        //↓Cube の 子オブジェクトである"Fog"オブジェクトから ParticleSystemコンポーネントを取得 
        ParticleObjFog = transform.FindChild("Fog").GetComponent<ParticleSystem>();
        //↓最終目的である rate にアクセスするために必要な emission を取得し格納
        mEmObjFog = ParticleObjFog.emission;

        //↓Cube の 子オブジェクトである"Fog"オブジェクトから ParticleSystemコンポーネントを取得 
        ParticleObjLightning = transform.FindChild("lightning").GetComponent<ParticleSystem>();

        //ParticleObjFog.Stop();
        ParticleObjLightning.Stop();


        //audioSource = gameObject.GetComponent<AudioSource>();
        sources = gameObject.GetComponents<AudioSource>();
        rain1 = sources[0];
        rain2 = sources[1];
        rain3 = sources[2];
        rain4 = sources[3];
        rain5 = sources[4];
        lightning = sources[5];


        m_MySliderValue = 0.5f;

        Count = 0;
    }

    // Update is called once per frame
 
    private float mCount = 0;       //←時間計測用
    private bool mSwitch = true;    //←切り替えスイッチ用

    void Update()
    {
        mCount = mCount + Time.deltaTime;   //←時間計測中
        /*if (mCount >= 5.0f)
        { //← 5秒経過する度に if 成立
            mCount = 0; // 時間計測用変数を初期化
            if (mSwitch == true)
            {
                //↓Rate を 800 に変更 強い雨
                mEmObj.rate = new ParticleSystem.MinMaxCurve(800);　//雨の生成量　雨の生成範囲に応じて高くしないといけないかも
                //audioSource.clip = audioClip1;
                //audioSource.Play();
                sources[1].Stop();　//弱い雨STOP
                sources[0].Play();　//強い雨用のaudiosurceファイルを再生
            }
            else
            {
                //↓Rate を 300 に変更 弱い雨
                mEmObj.rate = new ParticleSystem.MinMaxCurve(300);　//同上
                //audioSource.clip = audioClip2;
                //audioSource.Play();
                sources[0].Stop();　//強い雨STOP
                sources[1].Play();　//弱い雨用のaudiosurceファイルを再生
            }

            //↓ true が false に、false が true に交互に入れ替わり続ける
            mSwitch = !mSwitch;
        }*/

        if (Input.GetKeyDown(KeyCode.A))
        {
            Count = 1;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Count = 2;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Count = 3;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Count = 4;
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            Count = 5;
        }



        if (Count==1) //霧雨
        {
           // ParticleObjFog.Play();
            ParticleObjLightning.Stop();
            mEmObjRain.rate = new ParticleSystem.MinMaxCurve(100);
            mEmObjFog.rate = new ParticleSystem.MinMaxCurve(1);
            //rain2.Stop(); //弱い雨STOP
           // rain3.Stop();
            //rain4.Stop();
            //rain5.Stop();
            //sources[0].Play(); //弱い雨用のaudiosurceファイルを再生
            //audioSource.clip = audioClip1;
            rain1.Play();

        }
        else if (Count==2) //
        {
          //  ParticleObjFog.Play();
            ParticleObjLightning.Stop();
            mEmObjRain.rate = new ParticleSystem.MinMaxCurve(300);
            mEmObjFog.rate = new ParticleSystem.MinMaxCurve(3);
           // rain1.Stop(); //弱い雨STOP
            //rain3.Stop();
            //rain4.Stop();
            //rain5.Stop();
            //sources[1].Play();
            //audioSource.clip = audioClip2;
            rain2.Play();

        }
        else if (Count==3) //普通の雨
        {
          //  ParticleObjFog.Play();
            ParticleObjLightning.Stop();
            mEmObjRain.rate = new ParticleSystem.MinMaxCurve(800);
            mEmObjFog.rate = new ParticleSystem.MinMaxCurve(5);
            //rain1.Stop(); //弱い雨STOP
            //rain2.Stop();
            //rain4.Stop();
            //rain5.Stop();
            //sources[2].Play();
            //audioSource.clip = audioClip3;
            rain3.Play();

        }
        else if (Count==4) //
        {
           // ParticleObjFog.Play();
            ParticleObjLightning.Stop();
            mEmObjRain.rate = new ParticleSystem.MinMaxCurve(1100);
            mEmObjFog.rate = new ParticleSystem.MinMaxCurve(7);
            //rain1.Stop(); //弱い雨STOP
           // rain2.Stop();
            //rain3.Stop();
            //rain5.Stop();
            //sources[3].Play();
            //audioSource.clip = audioClip4;
            rain4.Play();

        }
        else if (Count==5) //すごく強い雨
        {
            //ParticleObjFog.Play();
            mEmObjRain.rate = new ParticleSystem.MinMaxCurve(2000);
            mEmObjFog.rate = new ParticleSystem.MinMaxCurve(50);
            //rain1.Stop(); //弱い雨STOP
            //rain2.Stop();
            //rain3.Stop();
            //rain4.Stop();
            //sources[3].Stop();
            //sources[4].Play(); //強い雨用のaudiosurceファイルを再生
            //audioSource.clip = audioClip5;
            rain5.Play();
            if (mCount >= 5.0f)
            { //← 5秒経過する度に if 成立
                mCount = 0; // 時間計測用変数を初期化
                if (mSwitch == true)
                {
                    lightning.Play();
                    ParticleObjLightning.Play();
                    
                }
                else
                {
                    lightning.Stop();
                    ParticleObjLightning.Stop();
                }

                //↓ true が false に、false が true に交互に入れ替わり続ける
                mSwitch = !mSwitch;
            }
        }











    }
}
