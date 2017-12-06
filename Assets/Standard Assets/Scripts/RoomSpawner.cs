using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{

    GameObject player;
    GameObject[] rooms;
    ArrayList combinations = new ArrayList();

    public GameObject leftRoom;
    public GameObject rightRoom;

    // Use this for initialization
    void Start()
    {
        rooms = GameObject.FindGameObjectsWithTag("Room");
        player = GameObject.FindGameObjectWithTag("Player");

        int[] arr = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
        int r = 2;
        int n = arr.GetLength(0);
        PrintCombination(arr, n, r);

    }

    IEnumerator waitSpawn()
    {
        while (true)
        {
            spawn();
            yield return new WaitForSeconds(3);
        }
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(waitSpawn());
    }

    int index = 0;

    void spawn()
    {
        if(index < combinations.Count - 1)
        {
            int left = (int)combinations[index];
            int right = (int)combinations[index+1];


            if (rooms[left - 1].GetComponent<Room>().isRight)
            {
                //flip it

            }

            if (rooms[right - 1].GetComponent<Room>().isLeft)
            {
                //flip it
            }

            SwapPrefabs(leftRoom, rooms[left - 1]);
            SwapPrefabs(rightRoom, rooms[right-1]);
            index = index + 2;
        }
    }

    /// <summary>Swaps the desired oldGameObject for a newPrefab.</summary>
    /// <param name="oldGameObject">The old game object.</param>
    void SwapPrefabs(GameObject oldGameObject, GameObject newPrefab)
    {
        // Determine the rotation and position values of the old game object.
        // Replace rotation with Quaternion.identity if you do not wish to keep rotation.
        Quaternion rotation = oldGameObject.transform.rotation;
        Vector3 position = oldGameObject.transform.position;

        // Instantiate the new game object at the old game objects position and rotation.
        GameObject newGameObject = Instantiate(newPrefab, position, rotation);

        // If the old game object has a valid parent transform,
        // (You can remove this entire if statement if you do not wish to ensure your
        // new game object does not keep the parent of the old game object.
        if (oldGameObject.transform.parent != null)
        {
            // Set the new game object parent as the old game objects parent.
            newGameObject.transform.SetParent(oldGameObject.transform.parent);
        }

        // Destroy the old game object, immediately, so it takes effect in the editor.
        DestroyImmediate(oldGameObject);
    }

    void PrintCombination(int[] arr, int n, int r)
    {
        int[] data = new int[r];
        CombinationUtility(arr, data, 0, n - 1, 0, r);
        /*
        for(int i = 0; i < combinations.Count; i+=2)
        {
            Debug.Log(combinations[i] + " " + combinations[i + 1]);
        }
        */
    }

    //Recursive function that computes all possible combinations
    void CombinationUtility(int[] arr, int[] data, int start, int end, int index, int r)
    {
        //Current Combination to be printed
        if (index == r)
        {
            for (int j = 0; j < r; j++)
            {
                combinations.Add(data[j]);
            }
            return;
        }

        for (int i = start; i <= end && end - i + 1 >= r - index; i++)
        {
            data[index] = arr[i];
            CombinationUtility(arr, data, i + 1, end, index + 1, r);
        }
    }

    public static int nCr(int n, int r)
    {
        // naive: return Factorial(n) / (Factorial(r) * Factorial(n - r));
        return nPr(n, r) / Factorial(r);
    }

    public static int nPr(int n, int r)
    {
        // naive: return Factorial(n) / Factorial(n - r);
        return FactorialDivision(n, n - r);
    }

    private static int FactorialDivision(int topFactorial, int divisorFactorial)
    {
        int result = 1;
        for (int i = topFactorial; i > divisorFactorial; i--)
            result *= i;
        return result;
    }

    private static int Factorial(int i)
    {
        if (i <= 1)
            return 1;
        return i * Factorial(i - 1);
    }

}
