import BusinessItem from '@/components/business-item'
import List from '@/components/list'
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card'
import { Input } from '@/components/ui/input'
import { Label } from '@/components/ui/label'
import { Separator } from '@/components/ui/separator'
import { Link, Mail, MapPin, Phone, UserRoundCog } from 'lucide-react'
import Image from 'next/image'
import React from 'react'

const values: string[] = ['Công nghệ thông tin', 'An Toàn thông tin']

export default function BusinessProfilePage() {
  return (
    <section className='w-full overflow-hidden dark:bg-gray-900'>
      <div className='w-full mx-auto'>
        {/* User Cover IMAGE */}
        <img
          src='https://images.unsplash.com/photo-1560697529-7236591c0066?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixid=M3w0NzEyNjZ8MHwxfHNlYXJjaHwxMHx8Y292ZXJ8ZW58MHwwfHx8MTcxMDQ4MTEwNnww&ixlib=rb-4.0.3&q=80&w=1080'
          alt='User Cover'
          className='w-full h-[20rem] sm:h-[13rem] md:h-[16rem] lg:h-[22rem] xl:h-[20rem]'
        />
        {/* User Profile Image */}
        <div className='w-full mx-auto flex pl-10 pr-10'>
          <img
            src='https://images.unsplash.com/photo-1463453091185-61582044d556?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixid=M3w0NzEyNjZ8MHwxfHNlYXJjaHwxMnx8cGVvcGxlfGVufDB8MHx8fDE3MTA0ODExOTN8MA&ixlib=rb-4.0.3&q=80&w=1080'
            alt='User Profile'
            className='rounded-full object-cover w-[8rem] h-[8rem] sm:w-[10rem] sm:h-[10rem] md:w-[12rem] md:h-[12rem] outline outline-2 outline-offset-2 outline-yellow-500 shadow-xl relative z-30 bottom-[4.3rem] sm:bottom-[5rem] md:bottom-[6rem]'
          />
          <div className='space-y-2 ml-5 mt-5'>
            <div className='flex items-center'>
              <h1 className='text-4xl font-bold uppercase'>Công ty cổ phần ABC</h1>
              <svg
                className='ml-2 w-5 h-5 me-2 text-blue-500 dark:text-blue-400 flex-shrink-0'
                aria-hidden='true'
                xmlns='http://www.w3.org/2000/svg'
                fill='currentColor'
                viewBox='0 0 20 20'
              >
                <path d='M10 .5a9.5 9.5 0 1 0 9.5 9.5A9.51 9.51 0 0 0 10 .5Zm3.707 8.207-4 4a1 1 0 0 1-1.414 0l-2-2a1 1 0 0 1 1.414-1.414L9 10.586l3.293-3.293a1 1 0 0 1 1.414 1.414Z' />
              </svg>
            </div>
            <div className='text-muted-foreground flex flex-col md:flex-row items-center'>
              <div className='flex items-center mb-2 md:mb-0'>
                <Link size={16} className='mr-2' />
                <a className='text-blue-500' href='https://facebook.com'>
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
      </div>
      <div className='grid gap-4 md:grid-cols-[1fr_250px] lg:grid-cols-3 lg:gap-8'>
        <div className='grid auto-rows-max items-start gap-4 lg:col-span-2 lg:gap-8'>
          <Card x-chunk='dashboard-07-chunk-0'>
            <CardHeader>
              <CardTitle className='text-lg'>Giới thiệu doanh nghiệp</CardTitle>
            </CardHeader>
            <CardContent>
              <span>
                Shopee là nền tảng thương mại điện tử ở Đông Nam Á và Đài Loan. Ra mắt năm 2015, nền tảng thương mại
                Shopee được xây dựng nhằm cung cấp cho người sử dùng những trải nghiệm dễ dàng, an toàn và nhanh chóng
                khi mua sắm trực tuyến thông qua hệ thống hỗ trợ thanh toán và vận hành vững mạnh. Chúng tôi có niềm tin
                mạnh mẽ rằng trải nghiệm mua sắm trực tuyến phải đơn giản, dễ dàng và mang đến cảm xúc vui thích. Niềm
                tin này truyền cảm hứng và thúc đẩy chúng tôi mỗi ngày tại Shopee.
              </span>
            </CardContent>
          </Card>
          <BusinessItem />
        </div>
        <div className='grid auto-rows-max items-start gap-4 lg:gap-8'>
          <Card x-chunk='dashboard-07-chunk-3'>
            <CardHeader>
              <CardTitle className='text-lg'>Lĩnh vực doanh nghiệp</CardTitle>
            </CardHeader>
            <CardContent>
              <List title='' list={values} />
            </CardContent>
          </Card>
          <Card x-chunk='dashboard-07-chunk-3'>
            <CardHeader>
              <CardTitle className='text-lg'>Danh sách chi nhánh</CardTitle>
            </CardHeader>
            <CardContent>
              <div className='flex items-center'>
                <h3 className='font-medium'>Chi nhánh 1</h3>
                <span className='ml-2 text-red-500'>{'(chi nhánh chính)'}</span>
              </div>

              <ul className='max-w-md space-y-1 text-gray-500 list-inside dark:text-gray-400 ml-5'>
                <li className='flex items-center'>
                  <MapPin size={20} className='mr-2' />
                  Hà nội
                </li>
                <li className='flex items-center'>
                  <Mail size={20} className='mr-2' />
                  dihson103@gmail.com
                </li>
                <li className='flex items-center'>
                  <Phone size={20} className='mr-2' />
                  0976099351
                </li>
              </ul>
              <Separator className='my-2' />
              <h3 className='font-medium'>Chi nhánh 2</h3>
              <ul className='max-w-md space-y-1 text-gray-500 list-inside dark:text-gray-400 ml-5'>
                <li className='flex items-center'>
                  <MapPin size={20} className='mr-2' />
                  Hà nội
                </li>
                <li className='flex items-center'>
                  <Mail size={20} className='mr-2' />
                  dihson103@gmail.com
                </li>
                <li className='flex items-center'>
                  <Phone size={20} className='mr-2' />
                  0976099351
                </li>
              </ul>
              <Separator className='my-2' />
              <h3 className='font-medium'>Chi nhánh 3</h3>
              <ul className='max-w-md space-y-1 text-gray-500 list-inside dark:text-gray-400 ml-5'>
                <li className='flex items-center'>
                  <MapPin size={20} className='mr-2' />
                  Hà nội
                </li>
                <li className='flex items-center'>
                  <Mail size={20} className='mr-2' />
                  dihson103@gmail.com
                </li>
                <li className='flex items-center'>
                  <Phone size={20} className='mr-2' />
                  0976099351
                </li>
              </ul>
            </CardContent>
          </Card>
        </div>
      </div>
    </section>
  )
}
