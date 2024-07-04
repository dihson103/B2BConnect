'use client'

import ImageDisplay from '@/components/image-choosed-display'
import { Button } from '@/components/ui/button'
import { Card, CardContent } from '@/components/ui/card'
import {
  Dialog,
  DialogClose,
  DialogContent,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger
} from '@/components/ui/dialog'
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from '@/components/ui/form'
import { Pencil, PlusCircle, Upload } from 'lucide-react'
import { useState } from 'react'
import { useForm } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import { CreateEventFormType, CreateEventSchema } from '@/rules/event.rules'
import { Input } from '@/components/ui/input'
import { Textarea } from '@/components/ui/textarea'
import { Event } from '@/types/event.types'
import { convertDateTimeToDisPlayInUpdateForm } from '@/lib/date'

type Props = {
  data: Event
}

export default function UpdateEventForm({ data }: Props) {
  const [image, setImage] = useState<string | null>(`http://localhost:7001/api/files/${data.image}`)
  const [fileMessage, setFileMessage] = useState<string | null>(null)

  const form = useForm<CreateEventFormType>({
    resolver: zodResolver(CreateEventSchema),
    defaultValues: {
      name: data.name,
      startDate: convertDateTimeToDisPlayInUpdateForm(data.startAt),
      endDate: convertDateTimeToDisPlayInUpdateForm(data.endAt),
      location: data.location,
      description: data.description,
      image: data.image
    }
  })

  const handleUploadPhoto = (e: React.ChangeEvent<HTMLInputElement>) => {
    const file = e.target.files ? e.target.files[0] : null
    setImage(file ? URL.createObjectURL(file) : null)
    setFileMessage(null)
  }

  const handleDelete = (index: number) => {
    setImage(null)
    setFileMessage('Bạn cần nhập ảnh sự kiện')
  }

  const onSubmit = (data: CreateEventFormType) => {
    setFileMessage(image ? null : 'Bạn cần nhập ảnh sự kiện')
    console.log('>>>> data', data)
  }

  return (
    <Dialog>
      <DialogTrigger asChild>
        <Button variant='ghost'>
          <Pencil className='h-5 w-5' />
        </Button>
      </DialogTrigger>
      <DialogContent className='sm:max-w-[800px]'>
        <DialogHeader>
          <DialogTitle>Tạo sự kiện mới</DialogTitle>
        </DialogHeader>
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
                        <ImageDisplay imageURLs={[image]} handleDelete={handleDelete} />
                      </div>
                    )}
                  </div>
                  <span className='text-[0.8rem] font-medium text-destructive'>{fileMessage}</span>
                </Card>
                <Card className='md:col-span-5 col-span-1'>
                  <CardContent className='relative'>
                    <div className='grid grid-cols-1 gap-2 mt-4'>
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
                  </CardContent>
                </Card>
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
            <DialogFooter>
              <DialogClose asChild>
                <Button type='button' variant='secondary'>
                  Hủy
                </Button>
              </DialogClose>
              <Button type='submit'>Tạo sự kiện</Button>
            </DialogFooter>
          </form>
        </Form>
      </DialogContent>
    </Dialog>
  )
}
