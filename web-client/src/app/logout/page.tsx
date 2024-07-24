'use client'

import { useRouter } from 'next/navigation'
import { useEffect } from 'react'

export default function LogoutPage() {
  const router = useRouter()
  useEffect(() => {
    fetch('http://localhost:3000/api/auth/test', {
      headers: {
        'Content-Type': 'application/json'
      }
    }).then((data) => {
      router.push('/login')
    })
  }, [router])
  return (
    <>
      <div role='status' className='space-y-2.5 animate-pulse max-w-lg'>
        <div className='flex items-center w-full'>
          <div className='h-2.5 bg-gray-200 rounded-full dark:bg-gray-700 w-32' />
          <div className='h-2.5 ms-2 bg-gray-300 rounded-full dark:bg-gray-600 w-24' />
          <div className='h-2.5 ms-2 bg-gray-300 rounded-full dark:bg-gray-600 w-full' />
        </div>
        <div className='flex items-center w-full max-w-[480px]'>
          <div className='h-2.5 bg-gray-200 rounded-full dark:bg-gray-700 w-full' />
          <div className='h-2.5 ms-2 bg-gray-300 rounded-full dark:bg-gray-600 w-full' />
          <div className='h-2.5 ms-2 bg-gray-300 rounded-full dark:bg-gray-600 w-24' />
        </div>
        <div className='flex items-center w-full max-w-[400px]'>
          <div className='h-2.5 bg-gray-300 rounded-full dark:bg-gray-600 w-full' />
          <div className='h-2.5 ms-2 bg-gray-200 rounded-full dark:bg-gray-700 w-80' />
          <div className='h-2.5 ms-2 bg-gray-300 rounded-full dark:bg-gray-600 w-full' />
        </div>
        <div className='flex items-center w-full max-w-[480px]'>
          <div className='h-2.5 ms-2 bg-gray-200 rounded-full dark:bg-gray-700 w-full' />
          <div className='h-2.5 ms-2 bg-gray-300 rounded-full dark:bg-gray-600 w-full' />
          <div className='h-2.5 ms-2 bg-gray-300 rounded-full dark:bg-gray-600 w-24' />
        </div>
        <div className='flex items-center w-full max-w-[440px]'>
          <div className='h-2.5 ms-2 bg-gray-300 rounded-full dark:bg-gray-600 w-32' />
          <div className='h-2.5 ms-2 bg-gray-300 rounded-full dark:bg-gray-600 w-24' />
          <div className='h-2.5 ms-2 bg-gray-200 rounded-full dark:bg-gray-700 w-full' />
        </div>
        <div className='flex items-center w-full max-w-[360px]'>
          <div className='h-2.5 ms-2 bg-gray-300 rounded-full dark:bg-gray-600 w-full' />
          <div className='h-2.5 ms-2 bg-gray-200 rounded-full dark:bg-gray-700 w-80' />
          <div className='h-2.5 ms-2 bg-gray-300 rounded-full dark:bg-gray-600 w-full' />
        </div>
        <span className='sr-only'>Loading...</span>
      </div>
    </>
  )
}
