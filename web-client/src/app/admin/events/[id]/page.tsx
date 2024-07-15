import * as React from 'react'
import { CheckIcon } from 'lucide-react'
import { Button } from '@/components/ui/button'
import { Card, CardContent, CardDescription, CardFooter, CardHeader, CardTitle } from '@/components/ui/card'
import { Tabs, TabsContent, TabsList, TabsTrigger } from '@/components/ui/tabs'
import Image from 'next/image'
import { getEventById } from '@/actions/event.actions'
import NotFoundPage from '@/components/not-found'
import { apiErrorHandler } from '@/lib/utils'

export default async function EventDetailPage({ params }: { params: { id: string } }) {
  try {
    const data = await getEventById(params.id)
    const event = data.data
    const industries = event.industries

    return (
      <main className='grid flex-1 items-start gap-4 p-4 sm:px-6 sm:py-0 md:gap-8 lg:grid-cols-3 xl:grid-cols-3'>
        <div>
          <Card className='w-full max-w-sm'>
            <div className='aspect-w-4 aspect-h-5 relative'>
              <Image
                src={`http://localhost:7001/api/files/${data.data.image}`}
                alt='Product'
                width={400}
                height={100}
                className='object-cover rounded-t-lg'
              />
            </div>
            <CardHeader className='grid gap-1 p-4'>
              <CardTitle className='text-center font-bold text-xl'>Thông tin sự kiện</CardTitle>
            </CardHeader>
            <CardContent className='pl-5 pr-5'>
              <div className='flex space-x-1'>
                <div className='font-semibold'>Địa điểm: </div>
                <div>Vân Lũng - An Khánh - Hoài Đức - Hà Nôi</div>
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
                <CardHeader className='px-7'>
                  <CardTitle>Chi tiết sự kiện</CardTitle>
                </CardHeader>
                <CardContent>{data.data.description}</CardContent>
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
