import { Card, CardContent } from '@/components/ui/card'
import { Link as LinkIcon, UserRoundCog } from 'lucide-react'
import Image from 'next/image'
import Link from 'next/link'
import React from 'react'

export default function BusinessItem() {
  return (
    <Card x-chunk='dashboard-07-chunk-1' className='hover:bg-slate-100 dark:hover:bg-slate-800'>
      <CardContent className='items-center p-4'>
        <div className='flex items-center'>
          <Image
            alt='Product image'
            className='aspect-square rounded-md object-cover mr-5'
            height='100'
            src='http://localhost:7001/api/files/638572427338586814.jpg'
            width='100'
          />
          <div>
            <Link
              href={'/businesses/1'}
              target='_blank'
              rel='noopener noreferrer'
              className='text-xl font-bold uppercase flex items-center'
            >
              CÔNG TY TNHH SẢN XUẤT VÀ THƯƠNG MẠI DONAGIFT
              <svg
                className='ml-2 w-4 h-4 me-2 text-blue-500 dark:text-blue-400 flex-shrink-0'
                aria-hidden='true'
                xmlns='http://www.w3.org/2000/svg'
                fill='currentColor'
                viewBox='0 0 20 20'
              >
                <path d='M10 .5a9.5 9.5 0 1 0 9.5 9.5A9.51 9.51 0 0 0 10 .5Zm3.707 8.207-4 4a1 1 0 0 1-1.414 0l-2-2a1 1 0 0 1 1.414-1.414L9 10.586l3.293-3.293a1 1 0 0 1 1.414 1.414Z' />
              </svg>
            </Link>

            <div className='text-muted-foreground flex flex-col md:flex-row items-center mt-3'>
              <div className='flex items-center mb-2 md:mb-0'>
                <LinkIcon size={16} className='mr-2' />
                <a className='text-blue-500' href='https://facebook.com' target='_blank' rel='noopener noreferrer'>
                  company-ABC.com
                </a>
              </div>
              <div className='flex items-center md:ml-6'>
                <UserRoundCog size={16} className='mr-2' />
                <p>50 - 100 nhân viên</p>
              </div>
            </div>
          </div>
        </div>
      </CardContent>
    </Card>
  )
}
