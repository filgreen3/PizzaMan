using UnityEngine;

public class PositionStruction {

    private Vector3[] line1, line2;
    private int id;
    private bool firstline;

    public PositionStruction (Vector3[] line1, Vector3[] line2) {
        this.line1 = line1;
        this.line2 = line2;
    }

    public Vector3 GetPosition {

        get {
            var lenght = (firstline) ? line1.Length : line2.Length;
            id = (id + Random.Range (0, lenght)) % lenght;
            firstline = !firstline;

            return (!firstline) ? line1[id] : line2[id];
        }
    }
}