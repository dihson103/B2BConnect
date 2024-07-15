'use client'

import ImageDisplay from '@/components/image-choosed-display'
import { Button } from '@/components/ui/button'
import { Card, CardContent, CardFooter, CardHeader, CardTitle } from '@/components/ui/card'
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from '@/components/ui/form'
import { BadgeMinus, Plus, PlusCircle, Upload } from 'lucide-react'
import { useState } from 'react'
import { useForm } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import { CreateEventFormType, CreateEventSchema } from '@/rules/event.rules'
import { Input } from '@/components/ui/input'
import { Textarea } from '@/components/ui/textarea'
import { Label } from '@/components/ui/label'
import { Badge } from '@/components/ui/badge'
import IndustryComboboxPopover from '@/components/combobox-industry-popover'
import { IndustryResponse } from '@/types/industry.types'
import { uploadFileAction } from '@/actions/file.action'
import { apiErrorHandler, apiResultHandler } from '@/lib/utils'
import { CreateEventType } from '@/types/event.types'
import { createEventAction } from '@/actions/event.actions'
import { useRouter } from 'next/navigation'
import { Tabs, TabsContent } from '@/components/ui/tabs'

export default function AddNewEventPage() {
  const [image, setImage] = useState<File | null>(null)
  const [fileMessage, setFileMessage] = useState<string | null>(null)
  const [industryMessage, setIndustryMessage] = useState<string | null>(null)
  const [industriesChoosed, setIndustriesChoosed] = useState<IndustryResponse[] | null>(null)
  const router = useRouter()

  const form = useForm<CreateEventFormType>({
    resolver: zodResolver(CreateEventSchema),
    defaultValues: {
      name: '',
      startDate: '',
      endDate: '',
      location: '',
      description: '',
      image: ''
    }
  })

  const handleUploadPhoto = (e: React.ChangeEvent<HTMLInputElement>) => {
    setImage(e.target.files ? e.target.files[0] : null)
    setFileMessage(null)
  }

  const handleDelete = (index: number) => {
    setImage(null)
    setFileMessage('Bạn cần nhập ảnh sự kiện')
  }

  const onSubmit = async (data: CreateEventFormType) => {
    if (image == null) {
      setFileMessage('Bạn cần nhập ảnh sự kiện')
      return
    }

    if (industriesChoosed == null || industriesChoosed.length === 0) {
      setIndustryMessage('Bạn cần chọn ít nhất 1 lĩnh vực')
      return
    }

    try {
      const imageBody = new FormData()
      imageBody.append('file', image)

      const response = await uploadFileAction(imageBody)
      const uploadedImage = response.data.fileName

      const body: CreateEventType = {
        name: data.name,
        description: data.description,
        startAt: new Date(data.startDate).toISOString(),
        endAt: new Date(data.endDate).toISOString(),
        location: data.location,
        image: uploadedImage,
        industryIds: industriesChoosed.map((industry) => industry.id)
      }

      const res = await createEventAction(body)
      apiResultHandler(res)

      form.reset()
      setImage(null)
      setIndustriesChoosed(null)
      router.push('/admin/events')
    } catch (error: any) {
      console.error(error.message)
      apiErrorHandler(error.message)
    }
  }

  const handleRemove = (id: string) => () => {
    setIndustriesChoosed((prev) => {
      if (prev === null) {
        return null
      }
      return prev.filter((industry) => industry.id !== id)
    })
  }
  return (
    <Tabs defaultValue='all'>
      <TabsContent value='all'>
        <Card x-chunk='dashboard-06-chunk-0'>
          <CardHeader>
            <CardTitle className='text-center font-bold text-3xl'>Thêm sự kiện</CardTitle>
          </CardHeader>
          <CardContent>
            <Form {...form}>
              <form onSubmit={form.handleSubmit(onSubmit)} className='w-full flex flex-col gap-4'>
                <div className='flex flex-col gap-6'>
                  <div className='grid grid-cols-1 gap-6 md:grid-cols-7'>
                    <Card className='w-full h-full md:col-span-2 col-span-1 relative'>
                      <div className='w-full h-full flex justify-center items-center p-3'>
                        {image === null ? (
                          <div className='w-full h-full flex justify-center items-center'>
                            <input
                              id='image'
                              type='file'
                              style={{ display: 'none' }}
                              accept='image/*'
                              onChange={(e) => handleUploadPhoto(e)}
                            />
                            <label htmlFor='image' className='flex flex-col items-center cursor-pointer'>
                              <Upload size={70} className='bg-primary-backgroudPrimary rounded-md p-5 mb-2' />
                              <span className='text-l text-gray-500 font-medium'>Hãy tải ảnh lên</span>
                            </label>
                          </div>
                        ) : (
                          <div className='relative w-full h-full flex justify-center items-center'>
                            <ImageDisplay
                              imageURLs={[image].map((i) => URL.createObjectURL(i))}
                              handleDelete={handleDelete}
                            />
                          </div>
                        )}
                      </div>
                      <span className='text-[0.8rem] font-medium text-destructive'>{fileMessage}</span>
                    </Card>
                    <div className='md:col-span-5 col-span-1'>
                      <div className='relative'>
                        <div className='grid grid-cols-1 gap-2'>
                          <FormField
                            control={form.control}
                            name='name'
                            render={({ field }) => {
                              return (
                                <FormItem>
                                  <FormLabel className='text-primary-backgroudPrimary'>Tên sự kiện</FormLabel>
                                  <FormControl>
                                    <Input type='text' placeholder='Tên sự kiện' {...field} />
                                  </FormControl>
                                  <FormMessage />
                                </FormItem>
                              )
                            }}
                          />

                          <div className='grid grid-cols-1 md:grid-cols-2 gap-4'>
                            <FormField
                              control={form.control}
                              name='startDate'
                              render={({ field }) => {
                                return (
                                  <FormItem>
                                    <FormLabel className='text-primary-backgroudPrimary'>Ngày bắt đầu</FormLabel>
                                    <FormControl>
                                      <Input type='datetime-local' {...field} />
                                    </FormControl>
                                    <FormMessage />
                                  </FormItem>
                                )
                              }}
                            />
                            <FormField
                              control={form.control}
                              name='endDate'
                              render={({ field }) => {
                                return (
                                  <FormItem>
                                    <FormLabel className='text-primary-backgroudPrimary'>Ngày kết thúc</FormLabel>
                                    <FormControl>
                                      <Input type='datetime-local' {...field} />
                                    </FormControl>
                                    <FormMessage />
                                  </FormItem>
                                )
                              }}
                            />
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <FormField
                    control={form.control}
                    name='location'
                    render={({ field }) => {
                      return (
                        <FormItem>
                          <FormLabel className='text-primary-backgroudPrimary'>Địa điểm tổ chức sự kiện</FormLabel>
                          <FormControl>
                            <Input type='text' placeholder='Địa điểm' {...field} />
                          </FormControl>
                          <FormMessage />
                        </FormItem>
                      )
                    }}
                  />
                  <div className='grid gap-2'>
                    <Label htmlFor='email'>Lĩnh vực trong sự kiện</Label>
                    <div>
                      {industriesChoosed?.map((industry) => (
                        <Badge variant='outline' key={industry.id} className='relative mr-2'>
                          {industry.name}
                          <BadgeMinus
                            onClick={handleRemove(industry.id)}
                            className='h-3 w-3 absolute top-0 right-0 transform translate-x-1/2 -translate-y-1/2 hover:cursor-pointer'
                          />
                        </Badge>
                      ))}
                    </div>
                    {(industriesChoosed == null || industriesChoosed.length === 0) && industryMessage != null ? (
                      <span className='text-[0.8rem] font-medium text-destructive'>{industryMessage}</span>
                    ) : (
                      <></>
                    )}
                    <IndustryComboboxPopover setIndustriesChoosed={setIndustriesChoosed} />
                  </div>

                  <FormField
                    control={form.control}
                    name='description'
                    render={({ field }) => {
                      return (
                        <FormItem>
                          <FormLabel className='text-primary-backgroudPrimary'>Thông tin chi tiết</FormLabel>
                          <FormControl>
                            <Textarea placeholder='Thông tin chi tiết' rows={7} {...field} />
                          </FormControl>
                          <FormMessage />
                        </FormItem>
                      )
                    }}
                  />
                </div>
                <div className='flex justify-end gap-2'>
                  <Button type='button' variant='secondary'>
                    Trở về
                  </Button>
                  <Button type='submit'>Chinhr sự kiện</Button>
                </div>
              </form>
            </Form>
          </CardContent>
        </Card>
      </TabsContent>
    </Tabs>
  )
}
