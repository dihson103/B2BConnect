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
import { BadgeMinus, Pencil, PlusCircle, Upload } from 'lucide-react'
import { useEffect, useState } from 'react'
import { useForm } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import { CreateEventFormType, CreateEventSchema, UpdateEventFormType, UpdateEventSchema } from '@/rules/event.rules'
import { Input } from '@/components/ui/input'
import { Textarea } from '@/components/ui/textarea'
import { Event, UpdateEventType } from '@/types/event.types'
import { convertDateTimeToDisPlayInUpdateForm } from '@/lib/date'
import { IndustryResponse } from '@/types/industry.types'
import { Label } from '@/components/ui/label'
import { Badge } from '@/components/ui/badge'
import IndustryComboboxPopover from '@/components/combobox-industry-popover'
import { getIndustriesByEventIdAction } from '@/actions/industry.Actions'
import { apiErrorHandler, apiResultHandler } from '@/lib/utils'
import { RadioGroup, RadioGroupItem } from '@/components/ui/radio-group'
import { uploadFileAction } from '@/actions/file.action'
import { updateEventAction } from '@/actions/event.actions'
import { useToast } from '@/components/ui/use-toast'

type Props = {
  data: Event
}

export default function UpdateEventForm({ data }: Props) {
  const { toast } = useToast()
  const [open, setOpen] = useState(false)
  const [image, setImage] = useState<string | File | null>(`http://localhost:7001/api/files/${data.image}`)
  const [fileMessage, setFileMessage] = useState<string | null>(null)
  const [industriesChoosed, setIndustriesChoosed] = useState<IndustryResponse[] | null>(null)
  const [industryMessage, setIndustryMessage] = useState<string | null>(null)

  useEffect(() => {
    getIndustriesByEventIdAction(data.id)
      .then((res) => {
        setIndustriesChoosed(res.data)
      })
      .catch((error: Error) => {
        apiErrorHandler(error.message)
      })
  }, [data.id])

  const form = useForm<UpdateEventFormType>({
    resolver: zodResolver(UpdateEventSchema),
    defaultValues: {
      name: data.name,
      startDate: convertDateTimeToDisPlayInUpdateForm(data.startAt),
      endDate: convertDateTimeToDisPlayInUpdateForm(data.endAt),
      location: data.location,
      description: data.description,
      image: data.image,
      status: data.status.toString()
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

  const onSubmit = async (formData: UpdateEventFormType) => {
    if (image == null) {
      setFileMessage('Bạn cần nhập ảnh sự kiện')
      return
    }

    if (industriesChoosed == null || industriesChoosed.length === 0) {
      setIndustryMessage('Bạn cần chọn ít nhất 1 lĩnh vực')
      return
    }

    try {
      const body: UpdateEventType = {
        name: formData.name,
        description: formData.description,
        location: formData.location,
        status: Number(formData.status),
        image: data.image,
        startAt: new Date(formData.startDate).toISOString(),
        endAt: new Date(formData.endDate).toISOString(),
        industryIds: industriesChoosed.map((industry) => industry.id)
      }
      if (image instanceof File) {
        const imageBody = new FormData()
        imageBody.append('file', image)
        const response = await uploadFileAction(imageBody)
        body.image = response.data.fileName
        console.log('>>>', response.data.fileName)
      }
      console.log('<<<', body)
      const res = await updateEventAction(data.id, body)

      apiResultHandler(res)

      form.reset()
      setImage(null)
      setIndustriesChoosed(null)
      setOpen(false)
    } catch (error: any) {
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
    <Dialog open={open} onOpenChange={setOpen}>
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
                        <ImageDisplay
                          imageURLs={[image].map((i) => (i instanceof File ? URL.createObjectURL(i) : i))}
                          handleDelete={handleDelete}
                        />
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

              <div className='grid gap-2'>
                <Label>Lĩnh vực trong sự kiện</Label>
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
                name='status'
                render={({ field }) => (
                  <FormItem className='space-y-3'>
                    <FormLabel>Trạng thái sự kiện</FormLabel>
                    <FormControl>
                      <RadioGroup
                        onValueChange={field.onChange}
                        defaultValue={field.value.toString()}
                        className='flex flex-col space-y-1'
                      >
                        <FormItem className='flex items-center space-x-3 space-y-0'>
                          <FormControl>
                            <RadioGroupItem value='0' />
                          </FormControl>
                          <FormLabel className='font-normal'>Sắp diễn ra</FormLabel>
                        </FormItem>
                        <FormItem className='flex items-center space-x-3 space-y-0'>
                          <FormControl>
                            <RadioGroupItem value='1' />
                          </FormControl>
                          <FormLabel className='font-normal'>Đang diễn ra</FormLabel>
                        </FormItem>
                        <FormItem className='flex items-center space-x-3 space-y-0'>
                          <FormControl>
                            <RadioGroupItem value='2' />
                          </FormControl>
                          <FormLabel className='font-normal'>Đã kết thúc</FormLabel>
                        </FormItem>
                        <FormItem className='flex items-center space-x-3 space-y-0'>
                          <FormControl>
                            <RadioGroupItem value='3' />
                          </FormControl>
                          <FormLabel className='font-normal'>Đã bị hủy</FormLabel>
                        </FormItem>
                      </RadioGroup>
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
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
              <Button type='submit'>Chỉnh sửa sự kiện</Button>
            </DialogFooter>
          </form>
        </Form>
      </DialogContent>
    </Dialog>
  )
}
