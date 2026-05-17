using System.Collections.Generic;
using UnityEngine;

public class RhythmSpawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnNote
    {
        public float tempo;
        public int lado; // 0 = esquerda | 1 = direita

        public SpawnNote(float tempo, int lado)
        {
            this.tempo = tempo;
            this.lado = lado;
        }
    }

    [Header("Prefabs")]
    public GameObject notaVermelhaPrefab;
    public GameObject notaAzulPrefab;

    [Header("Spawners")]
    public Transform spawnerEsquerda;
    public Transform spawnerDireita;

    [Header("Sincronização")]
    public float offset = 0f;

    private List<SpawnNote> notas = new List<SpawnNote>();

    private float timer;
    private int indiceAtual;

    void Start()
    {
        // Verificações importantes
        if (notaVermelhaPrefab == null)
            Debug.LogError("notaVermelhaPrefab não foi atribuído!");

        if (notaAzulPrefab == null)
            Debug.LogError("notaAzulPrefab não foi atribuído!");

        if (spawnerEsquerda == null)
            Debug.LogError("spawnerEsquerda não foi atribuído!");

        if (spawnerDireita == null)
            Debug.LogError("spawnerDireita não foi atribuído!");

        notas = new List<SpawnNote>()
        {
            // LOOP 1
            new SpawnNote(0.50f, 0),
            new SpawnNote(0.65f, 1),
            new SpawnNote(1.00f, 0),
            new SpawnNote(1.24f, 1),
            new SpawnNote(1.85f, 0),
            new SpawnNote(2.44f, 1),
            new SpawnNote(3.00f, 0),
            new SpawnNote(4.30f, 1),
            new SpawnNote(4.64f, 0),
            new SpawnNote(4.85f, 1),
            new SpawnNote(5.45f, 0),
            new SpawnNote(6.05f, 1),
            new SpawnNote(6.65f, 0),
            new SpawnNote(7.25f, 1),
            new SpawnNote(7.84f, 0),
            new SpawnNote(8.45f, 1),
            new SpawnNote(9.05f, 0),
            new SpawnNote(9.65f, 1),

            new SpawnNote(10.24f, 0),
            new SpawnNote(10.41f, 1),
            new SpawnNote(10.55f, 0),
            new SpawnNote(10.73f, 1),
            new SpawnNote(10.84f, 0),

            new SpawnNote(11.45f, 1),
            new SpawnNote(12.05f, 0),
            new SpawnNote(12.64f, 1),
            new SpawnNote(13.24f, 0),
            new SpawnNote(13.84f, 1),

            new SpawnNote(14.44f, 0),
            new SpawnNote(14.61f, 1),
            new SpawnNote(14.75f, 0),
            new SpawnNote(14.91f, 1),
            new SpawnNote(15.04f, 0),

            new SpawnNote(15.64f, 1),
            new SpawnNote(16.24f, 0),
            new SpawnNote(16.84f, 1),
            new SpawnNote(17.44f, 0),
            new SpawnNote(18.04f, 1),

            new SpawnNote(18.21f, 0),
            new SpawnNote(18.34f, 1),
            new SpawnNote(18.51f, 0),
            new SpawnNote(18.65f, 1),

            new SpawnNote(19.10f, 0),

            // LOOP 2
            new SpawnNote(19.60f, 0),
            new SpawnNote(19.75f, 1),
            new SpawnNote(20.10f, 0),
            new SpawnNote(20.34f, 1),
            new SpawnNote(20.95f, 0),
            new SpawnNote(21.54f, 1),
            new SpawnNote(22.10f, 0),
            new SpawnNote(23.40f, 1),
            new SpawnNote(23.74f, 0),
            new SpawnNote(23.95f, 1),
            new SpawnNote(24.55f, 0),
            new SpawnNote(25.15f, 1),
            new SpawnNote(25.75f, 0),
            new SpawnNote(26.35f, 1),
            new SpawnNote(26.94f, 0),
            new SpawnNote(27.55f, 1),
            new SpawnNote(28.15f, 0),
            new SpawnNote(28.75f, 1),

            new SpawnNote(29.34f, 0),
            new SpawnNote(29.51f, 1),
            new SpawnNote(29.65f, 0),
            new SpawnNote(29.83f, 1),
            new SpawnNote(29.94f, 0),

            new SpawnNote(30.55f, 1),
            new SpawnNote(31.15f, 0),
            new SpawnNote(31.74f, 1),
            new SpawnNote(32.34f, 0),
            new SpawnNote(32.94f, 1),

            new SpawnNote(33.54f, 0),
            new SpawnNote(33.71f, 1),
            new SpawnNote(33.85f, 0),
            new SpawnNote(34.01f, 1),
            new SpawnNote(34.14f, 0),

            new SpawnNote(34.74f, 1),
            new SpawnNote(35.34f, 0),
            new SpawnNote(35.94f, 1),
            new SpawnNote(36.54f, 0),
            new SpawnNote(37.14f, 1),

            new SpawnNote(37.31f, 0),
            new SpawnNote(37.44f, 1),
            new SpawnNote(37.61f, 0),
            new SpawnNote(37.75f, 1),

            new SpawnNote(38.20f, 0),
        };
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (indiceAtual >= notas.Count)
            return;

        SpawnNote notaAtual = notas[indiceAtual];

        // Spawn sincronizado com offset
        if (timer >= notaAtual.tempo + offset)
        {
            SpawnarNota(notaAtual);
            indiceAtual++;
        }
    }

    void SpawnarNota(SpawnNote nota)
    {
        GameObject prefabDaNota = null;
        Transform spawnPoint = null;

        // Nota esquerda = vermelha
        if (nota.lado == 0)
        {
            prefabDaNota = notaVermelhaPrefab;
            spawnPoint = spawnerEsquerda;
        }
        // Nota direita = azul
        else
        {
            prefabDaNota = notaAzulPrefab;
            spawnPoint = spawnerDireita;
        }

        // Segurança contra null
        if (prefabDaNota == null)
        {
            Debug.LogError("Prefab da nota está nulo!");
            return;
        }

        if (spawnPoint == null)
        {
            Debug.LogError("SpawnPoint está nulo!");
            return;
        }

        // Instancia nota
        GameObject clone = Instantiate(
            prefabDaNota,
            spawnPoint.position,
            Quaternion.identity
        );

        // Destrói após 5 segundos
        Destroy(clone, 5f);
    }
}