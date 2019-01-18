using System;

[Serializable]
public struct TileState {
    public int ownerId;
    public State structureState;
    public Direction direction;
    public bool isDestroyed;

    public TileState(int ownerId, State state, Direction direction, bool isDestroyed) {
        this.ownerId = ownerId;
        this.structureState = state;
        this.direction = direction;
        this.isDestroyed = isDestroyed;
    }

    public enum State {
        On,
        Off
    }

    public static bool operator == (TileState a, TileState b) {
        return a.ownerId == b.ownerId && a.structureState == b.structureState && a.isDestroyed == b.isDestroyed && a.direction == b.direction;
    }

    public static bool operator != (TileState a, TileState b) {
        return !(a == b);
    }

    public override bool Equals(object obj) {
        if (obj is TileState) {
            TileState state = (TileState)obj;
            return ownerId == state.ownerId && structureState == state.structureState && isDestroyed == state.isDestroyed && direction == state.direction;
        }
        return false;
    }

    public bool Equals(TileState state) {
        return ownerId == state.ownerId && structureState == state.structureState && isDestroyed == state.isDestroyed && direction == state.direction;
    }

    public override int GetHashCode() {
        return ShiftAndWrap(ShiftAndWrap(ownerId.GetHashCode(), 2) ^ structureState.GetHashCode(), 2) ^ (ShiftAndWrap(direction.GetHashCode(), 2) ^ isDestroyed.GetHashCode());
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
