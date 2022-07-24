package com.example.metalhead_testap

import android.view.LayoutInflater
import android.view.ViewGroup
import android.view.inputmethod.InputBinding
import android.widget.Button
import android.widget.Toast
import androidx.core.net.toUri
import androidx.recyclerview.widget.AsyncListDiffer
import androidx.recyclerview.widget.DiffUtil
import androidx.recyclerview.widget.RecyclerView
import com.bumptech.glide.Glide
import com.example.metalhead_testap.databinding.FragmentSearchBinding
import com.example.metalhead_testap.databinding.ItemAlbumBinding
import com.google.android.material.dialog.MaterialAlertDialogBuilder
import com.google.android.material.snackbar.Snackbar

class AlbumAdapter: RecyclerView.Adapter<AlbumAdapter.AlbumViewHolder>() {

    inner class AlbumViewHolder(val binding: ItemAlbumBinding) :
        RecyclerView.ViewHolder(binding.root)

    private lateinit var binding: FragmentSearchBinding

    private val difCallback = object : DiffUtil.ItemCallback<Album>() {
        override fun areItemsTheSame(oldItem: Album, newItem: Album): Boolean {
            return oldItem.idAlbum == newItem.idAlbum
        }

        override fun areContentsTheSame(oldItem: Album, newItem: Album): Boolean {
            return oldItem == newItem
        }
    }

    private val differ = AsyncListDiffer(this, difCallback)
    var albums: List<Album>
        get() = differ.currentList
        set(value) {
            differ.submitList(value)
        }


    override fun getItemCount() = albums.size

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): AlbumViewHolder {
        return AlbumViewHolder(
            ItemAlbumBinding.inflate(
                LayoutInflater.from(parent.context),
                parent,
                false
            )
        )
    }

    fun getPosition(position: Int) {
        albums[position]

    }

    override fun onBindViewHolder(holder: AlbumViewHolder, position: Int) {
        val albumS = albums[position]
        holder.binding.apply {
            val album = albums[position]
            Glide.with(itemAlbumImage).load(albums[position].image).into(itemAlbumImage)
            itemAlbumName.text = album.name
            itemAlbumArtist.text = album.artist
        }


        holder.itemView.findViewById<Button>(R.id.infobutton).setOnClickListener {
            val position: Int = holder.layoutPosition
            val albumID = albums[position].idAlbum
            Toast.makeText(holder.itemView.context, "albumId is : ${albumID}", Toast.LENGTH_SHORT)
                .show()
        }


    }
}

