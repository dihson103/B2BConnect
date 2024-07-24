'use client'

import { ImageResponse } from '@/types/event.types'
import Image from 'next/image'
import React, { useState, useEffect, useCallback } from 'react'

type Props = {
  images: ImageResponse[]
}

export default function Carousel({ images }: Props) {
  const [activeIndex, setActiveIndex] = useState(0)

  const handlePrev = useCallback(() => {
    setActiveIndex((prevIndex) => (prevIndex === 0 ? images.length - 1 : prevIndex - 1))
  }, [images.length])

  const handleNext = useCallback(() => {
    setActiveIndex((prevIndex) => (prevIndex === images.length - 1 ? 0 : prevIndex + 1))
  }, [images.length])

  useEffect(() => {
    const interval = setInterval(() => {
      handleNext()
    }, 5000)
    return () => clearInterval(interval)
  }, [handleNext])

  return (
    <div id='default-carousel' className='relative w-full' data-carousel='slide'>
      {/* Carousel wrapper */}
      <div className='relative h-56 overflow-hidden rounded-lg md:h-96'>
        {images.map((image, index) => (
          <div
            key={image.id}
            className={`absolute inset-0 transition-opacity duration-700 ease-in-out ${index === activeIndex ? 'opacity-100' : 'opacity-0'}`}
            data-carousel-item
          >
            <Image
              src={`http://localhost:7001/api/files/${image.path}`}
              alt='Product'
              layout='fill'
              objectFit='cover'
              className='block w-full h-full'
            />
          </div>
        ))}
      </div>

      {/* Slider controls */}
      <button
        type='button'
        className='absolute top-0 left-0 z-30 flex items-center justify-center h-full px-4 cursor-pointer group focus:outline-none'
        onClick={handlePrev}
      >
        <span className='inline-flex items-center justify-center w-10 h-10 rounded-full bg-white/30 dark:bg-gray-800/30 group-hover:bg-white/50 dark:group-hover:bg-gray-800/60 group-focus:ring-4 group-focus:ring-white dark:group-focus:ring-gray-800/70 group-focus:outline-none'>
          <svg
            className='w-4 h-4 text-white dark:text-gray-800'
            aria-hidden='true'
            xmlns='http://www.w3.org/2000/svg'
            fill='none'
            viewBox='0 0 6 10'
          >
            <path stroke='currentColor' strokeLinecap='round' strokeLinejoin='round' strokeWidth={2} d='M5 1 1 5l4 4' />
          </svg>
          <span className='sr-only'>Previous</span>
        </span>
      </button>
      <button
        type='button'
        className='absolute top-0 right-0 z-30 flex items-center justify-center h-full px-4 cursor-pointer group focus:outline-none'
        onClick={handleNext}
      >
        <span className='inline-flex items-center justify-center w-10 h-10 rounded-full bg-white/30 dark:bg-gray-800/30 group-hover:bg-white/50 dark:group-hover:bg-gray-800/60 group-focus:ring-4 group-focus:ring-white dark:group-focus:ring-gray-800/70 group-focus:outline-none'>
          <svg
            className='w-4 h-4 text-white dark:text-gray-800'
            aria-hidden='true'
            xmlns='http://www.w3.org/2000/svg'
            fill='none'
            viewBox='0 0 6 10'
          >
            <path stroke='currentColor' strokeLinecap='round' strokeLinejoin='round' strokeWidth={2} d='m1 9 4-4-4-4' />
          </svg>
          <span className='sr-only'>Next</span>
        </span>
      </button>
    </div>
  )
}
