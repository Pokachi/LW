using System;

/// <summary>
/// Structure used to represent a point on the tilemap
/// </summary>
[Serializable]
public struct Point {
    public int x;
    public int y;

    public Point (int x, int y) {
        this.x = x;
        this.y = y;
    }

    public static Point operator + (Point a, Point b) {
        return new Point(a.x + b.x, a.y + b.y);
    }

    public static Point operator - (Point p1, Point p2) {
        return new Point(p1.x - p2.x, p1.y - p2.y);
    }

    public static bool operator == (Point a, Point b) {
        return a.x == b.x && a.y == b.y;
    }

    public static bool operator != (Point a, Point b) {
        return !(a == b);
    }

    public override string ToString() {
        return string.Format("({0},{1})", x, y);
    }

    public override bool Equals(object obj) {
        if (obj is Point) {
            Point p = (Point)obj;
            return x == p.x && y == p.y;
        }
        return false;
    }

    public bool Equals(Point p) {
        return x == p.x && y == p.y;
    }

    public override int GetHashCode() {
        return ShiftAndWrap(x.GetHashCode(), 2) ^ y.GetHashCode();
    }

    private int ShiftAndWrap(int value, int positions) {
        positions = positions & 0x1F;

        // Save the existing bit pattern, but interpret it as an unsigned integer.
        uint number = BitConverter.ToUInt32(System.BitConverter.GetBytes(value), 0);
        // Preserve the bits to be discarded.
        uint wrapped = number >> (32 - positions);
        // Shift and wrap the discarded bits.
        return BitConverter.ToInt32(BitConverter.GetBytes((number << positions) | wrapped), 0);
    }
}