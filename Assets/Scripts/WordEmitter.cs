using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordEmitter : MonoBehaviour
{
    [SerializeField]
    public List<string> EmittedWords = new List<string>(){
        "word1"
    };

    [SerializeField]
    public int DurationInFrames = 300;

    [SerializeField]
    public float StartScale = 0f;
    [SerializeField]
    public float EndScale = .5f;
    
    [SerializeField]
    public Vector3 StartOffset = new Vector3(0,1,0);
    [SerializeField]
    public Vector3 EndOffset = new Vector3(0,2,0);

    [SerializeField]
    public Color StartColor = new Color(1,1,1,1);
    [SerializeField]
    public Color EndColor = new Color(1,1,1,0);

    public void SpawnText() {
        Debug.Log("Howdy");
        GameObject wordEmitManagerGameObject = new GameObject("WordEmitManager");
        wordEmitManagerGameObject.AddComponent<WordEmitManager>();
        wordEmitManagerGameObject.AddComponent<RectTransform>();
        wordEmitManagerGameObject.transform.position = transform.position;
        WordEmitManager wordEmitManager = wordEmitManagerGameObject.GetComponent<WordEmitManager>();
        wordEmitManager.EmittedWord = EmittedWords[(int)(Random.value*EmittedWords.Count)];
        wordEmitManager.DurationInFrames = DurationInFrames;
        wordEmitManager.StartScale = StartScale;
        wordEmitManager.EndScale = EndScale;
        wordEmitManager.StartOffset = StartOffset;
        wordEmitManager.EndOffset = EndOffset;
        wordEmitManager.StartColor = StartColor;
        wordEmitManager.EndColor = EndColor;
    }

    public class WordEmitManager : MonoBehaviour {

        public string EmittedWord;
        public int DurationInFrames;
        public float StartScale;
        public float EndScale;
        public Vector3 StartOffset;
        public Vector3 EndOffset;
        public Color StartColor;
        public Color EndColor;

        private GameObject SpawnedTextObject;
        private int StartFrameNum;
        private float ScaleStep;
        private Vector3 OffsetStep;
        private Vector4 ColorStep;

        public void Start() {

            SpawnedTextObject = new GameObject(EmittedWord);

            //SpawnedTextObject.transform.parent = parentRef;

            SpawnedTextObject.AddComponent<RectTransform>();

            Vector3 myPos = transform.position;
            SpawnedTextObject.transform.position = new Vector3(myPos.x+StartOffset.x, myPos.y+StartOffset.y, myPos.z+StartOffset.z);
            SpawnedTextObject.transform.localScale = new Vector3(StartScale,StartScale,StartScale);

            SpawnedTextObject.AddComponent<TextMeshPro>();
            SpawnedTextObject.GetComponent<TextMeshPro>().text = EmittedWord;
            SpawnedTextObject.GetComponent<TextMeshPro>().alignment = TextAlignmentOptions.Center;
            SpawnedTextObject.GetComponent<TextMeshPro>().color = StartColor;
            //Need to fix layer SpawnedTextObject.GetComponent<TextMeshPro>().

            StartFrameNum = Time.frameCount;
            ScaleStep = (StartScale-EndScale)/DurationInFrames;
            OffsetStep = (EndOffset-StartOffset)/DurationInFrames;
            ColorStep = (EndColor-StartColor)/DurationInFrames;
        }

        // Update is called once per frame
        void Update() {
            if (SpawnedTextObject != null) {
                SpawnedTextObject.transform.position += OffsetStep;
                SpawnedTextObject.transform.localScale -= new Vector3(ScaleStep,ScaleStep,ScaleStep);

                TextMeshPro textMesh = SpawnedTextObject.GetComponent<TextMeshPro>();
                textMesh.color = (Vector4)textMesh.color+ColorStep;

                if (Time.frameCount > StartFrameNum+DurationInFrames) {
                    Destroy(SpawnedTextObject);
                    Destroy(this.gameObject);
                }
            }
        }

        void OnDestroy() {
            if (SpawnedTextObject != null) {
                Destroy(SpawnedTextObject);
            }
        }
    }
}