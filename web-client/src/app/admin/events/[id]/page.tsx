import * as React from 'react'
import { CheckIcon } from 'lucide-react'
import { Button } from '@/components/ui/button'
import { Card, CardContent, CardDescription, CardFooter, CardHeader, CardTitle } from '@/components/ui/card'
import { Tabs, TabsContent, TabsList, TabsTrigger } from '@/components/ui/tabs'
import Image from 'next/image'
import { getEventById } from '@/actions/event.actions'
import NotFoundPage from '@/components/not-found'
import { apiErrorHandler } from '@/lib/utils'
import Carousel from '@/components/carousel'
import List from '@/components/list'

export default async function EventDetailPage({ params }: { params: { id: string } }) {
  try {
    const data = await getEventById(params.id)

    return (
      <main className='grid flex-1 items-start gap-4 p-4 sm:px-6 sm:py-0 md:gap-8 lg:grid-cols-3 xl:grid-cols-3'>
        <div>
          <Card className='w-full max-w-sm'>
            <div className='aspect-w-4 aspect-h-5 relative'>
              <Carousel images={data.data.images} />
            </div>
            <CardHeader className='grid gap-1 p-4'>
              <CardTitle className='text-center font-bold text-xl'>Thông tin sự kiện</CardTitle>
            </CardHeader>
            <CardContent className='pl-5 pr-5'>
              <div className='flex space-x-1'>
                <div className='font-semibold'>Địa điểm: </div>
                <div>{data.data.location}</div>
              </div>
              <div className='flex space-x-1 mt-2'>
                <div className='font-semibold'>Bắt đầu: </div>
                <div>{data.data.startAt}</div>
              </div>
              <div className='flex space-x-1 mt-2'>
                <div className='font-semibold'>Kết thúc: </div>
                <div>{data.data.endAt}</div>
              </div>
              <div className='flex space-x-1 mt-2'>
                <div className='font-semibold'>Trạng thái: </div>
                <div>{data.data.statusDescription}</div>
              </div>
            </CardContent>
            <CardFooter>
              <Button className='w-full'>
                <CheckIcon className='mr-2 h-4 w-4' /> Tham gia sự kiện
              </Button>
            </CardFooter>
          </Card>
        </div>
        <div className='grid auto-rows-max items-start gap-4 md:gap-8 lg:col-span-2'>
          <Tabs defaultValue='week'>
            <div className='flex items-center'>
              <span className='text-center font-bold text-3xl'>{data.data.name}</span>
            </div>
            <TabsContent value='week'>
              <Card x-chunk='dashboard-05-chunk-3'>
                <CardContent className='mt-5'>
                  <h2 className='mb-2 text-lg font-semibold text-gray-900 dark:text-white'>Chi tiết sự kiện:</h2>
                  {data.data.description}
                </CardContent>
                <CardContent>
                  <List title='Các lĩnh vực trong sự kiện:' list={data.data.industries.map((i) => i.name)} />
                </CardContent>
              </Card>
            </TabsContent>
          </Tabs>
        </div>
      </main>
    )
  } catch (error: any) {
    const infor = error.message.split('|')
    const status = infor[0]
    const message = infor[1]

    if (status === '500') {
      return (
        <Tabs defaultValue='all'>
          <TabsContent value='all'>
            <Card x-chunk='dashboard-06-chunk-0'>
              <NotFoundPage />
            </Card>
          </TabsContent>
        </Tabs>
      )
    } else if (status === '400') {
      return <NotFoundPage />
    } else {
      apiErrorHandler(message)
    }
  }
}
