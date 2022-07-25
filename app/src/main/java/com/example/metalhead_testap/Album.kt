package com.example.metalhead_testap

import android.os.Parcel
import android.os.Parcelable


data class Album(
    var idAlbum: String,
    var name: String,
    var artist: String,
    var image: String,
    var totalTracks: Int,
    var releaseDate: String,
    var totRating: Double
): Parcelable {
    constructor(parcel: Parcel) : this(
        parcel.readString()!!,
        parcel.readString()!!,
        parcel.readString()!!,
        parcel.readString()!!,
        parcel.readInt(),
        parcel.readString()!!,
        parcel.readDouble()
    ) {
    }

    override fun writeToParcel(parcel: Parcel, flags: Int) {
        parcel.writeString(idAlbum)
        parcel.writeString(name)
        parcel.writeString(artist)
        parcel.writeString(image)
        parcel.writeInt(totalTracks)
        parcel.writeString(releaseDate)
        parcel.writeDouble(totRating)
    }

    override fun describeContents(): Int {
        return 0
    }

    companion object CREATOR : Parcelable.Creator<Album> {
        override fun createFromParcel(parcel: Parcel): Album {
            return Album(parcel)
        }

        override fun newArray(size: Int): Array<Album?> {
            return arrayOfNulls(size)
        }
    }
}
