import Image from 'next/image'
import Link from 'next/link'

export default function NotFoundPage() {
  return (
    <div className='flex flex-col justify-center items-center px-6 mx-auto h-screen xl:px-0 dark:bg-gray-900'>
      <div className='block md:max-w-lg'>
        <Image
          src='https://flowbite.com/application-ui/demo/images/illustrations/404.svg'
          width={500}
          height={300}
          alt='astronaut image'
        />
      </div>
      <div className='text-center xl:max-w-4xl'>
        <h1 className='mb-3 text-2xl font-bold leading-tight text-gray-900 sm:text-4xl lg:text-5xl dark:text-white'>
          Không tìm thấy trang
        </h1>
      </div>
    </div>
  )
}
