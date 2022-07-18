package com.example.metalhead_testap

import android.view.LayoutInflater
import android.view.ViewGroup
import android.view.inputmethod.InputBinding
import androidx.core.net.toUri
import androidx.recyclerview.widget.AsyncListDiffer
import androidx.recyclerview.widget.DiffUtil
import androidx.recyclerview.widget.RecyclerView
import com.example.metalhead_testap.databinding.ItemAlbumBinding

class AlbumAdapter: RecyclerView.Adapter<AlbumAdapter.AlbumViewHolder>() {

    inner class AlbumViewHolder(val binding: ItemAlbumBinding) : RecyclerView.ViewHolder(binding.root)

    private val difCallback = object : DiffUtil.ItemCallback<Album>(){
        override fun areItemsTheSame(oldItem: Album, newItem: Album): Boolean {
            return oldItem.idAlbum == newItem.idAlbum
        }

        override fun areContentsTheSame(oldItem: Album, newItem: Album): Boolean {
            return oldItem == newItem
        }
    }

    private val differ = AsyncListDiffer(this, difCallback)
    var albums : List<Album>
        get() = differ.currentList
        set(value) { differ.submitList(value) }

    override fun getItemCount() = albums.size

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): AlbumViewHolder {
        return AlbumViewHolder(ItemAlbumBinding.inflate(LayoutInflater.from(parent.context), parent, false ))
    }

    override fun onBindViewHolder(holder: AlbumViewHolder, position: Int) {
        holder.binding.apply {
            val album = albums[position]
            itemAlbumImage.setImageURI(albums[position].image.toUri())
            itemAlbumName.text = album.name
            itemAlbumArtist.text = album.artist
        }
    }
}