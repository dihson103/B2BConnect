'use client'

import { Button } from '@/components/ui/button'
import { CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from '@/components/ui/form'
import { BadgeMinus } from 'lucide-react'
import { useState } from 'react'
import { useForm } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import { UpdateEventFormType, UpdateEventSchema } from '@/rules/event.rules'
import { Input } from '@/components/ui/input'
import { Textarea } from '@/components/ui/textarea'
import { EventDetail, ImageRequest, UpdateEventType } from '@/types/event.types'
import { convertDateTimeToDisPlayInUpdateForm } from '@/lib/date'
import { IndustryResponse } from '@/types/industry.types'
import { Label } from '@/components/ui/label'
import { Badge } from '@/components/ui/badge'
import IndustryComboboxPopover from '@/components/combobox-industry-popover'
import { apiErrorHandler, apiResultHandler } from '@/lib/utils'
import { RadioGroup, RadioGroupItem } from '@/components/ui/radio-group'
import UploadFiles from '@/components/upload-file'
import { uploadFileAction } from '@/actions/file.action'
import { updateEventAction } from '@/actions/event.actions'
import { useRouter } from 'next/navigation'
import { useToast } from '@/components/ui/use-toast'

type Props = {
  data: EventDetail
  industries: IndustryResponse[]
}

export default function UpdateEventPage({ data, industries }: Props) {
  const [files, setFiles] = useState<(File | string)[]>(data.images.map((i) => i.path))
  const [mainImageIndex, setMainImageIndex] = useState<number | null>(data.images.findIndex((image) => image.isMain))
  const [fileMessage, setFileMessage] = useState<string | null>(null)
  const [industriesChoosed, setIndustriesChoosed] = useState<IndustryResponse[] | null>(industries)
  const [industryMessage, setIndustryMessage] = useState<string | null>(null)
  const router = useRouter()
  const { toast } = useToast()

  const form = useForm<UpdateEventFormType>({
    resolver: zodResolver(UpdateEventSchema),
    defaultValues: {
      name: data.name,
      startDate: convertDateTimeToDisPlayInUpdateForm(data.startAt),
      endDate: convertDateTimeToDisPlayInUpdateForm(data.endAt),
      location: data.location,
      description: data.description,
      status: data.status.toString()
    }
  })

  const onSubmit = async (formData: UpdateEventFormType) => {
    if (files.length == 0) {
      setFileMessage('Bạn cần nhập ảnh sự kiện')
      return
    }

    if (industriesChoosed == null || industriesChoosed.length === 0) {
      setIndustryMessage('Bạn cần chọn ít nhất 1 lĩnh vực')
      return
    }

    try {
      const imageBody = new FormData()
      files.forEach((file) => {
        if (file instanceof File) {
          imageBody.append('receivedFiles', file)
        }
      })

      const response = await uploadFileAction(imageBody)
      const uploadedImages = response.data

      let fileIndex = -1
      const imageRequests: ImageRequest[] = files.map((file, index) => {
        if (file instanceof File) fileIndex++
        return {
          image: file instanceof File ? uploadedImages[fileIndex] : file,
          isMain: index === mainImageIndex
        }
      })

      console.log('image', imageRequests)

      const body: UpdateEventType = {
        name: formData.name,
        description: formData.description,
        location: formData.location,
        status: Number(formData.status),
        imageRequests: imageRequests,
        startAt: new Date(formData.startDate).toISOString(),
        endAt: new Date(formData.endDate).toISOString(),
        industryIds: industriesChoosed.map((industry) => industry.id)
      }
      console.log('<<<', body)
      const res = await updateEventAction(data.id, body)

      apiResultHandler(res)
      router.push('/admin/events')
    } catch (error: any) {
      toast({
        title: 'Lỗi',
        description: error.message ?? 'Lỗi không xác định',
        duration: 5000,
        variant: 'destructive'
      })
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
    <>
      <CardHeader>
        <CardTitle className='text-center font-bold text-3xl'>Chỉnh sửa sự kiện</CardTitle>
      </CardHeader>
      <CardContent>
        <Form {...form}>
          <form onSubmit={form.handleSubmit(onSubmit)} className='w-full flex flex-col gap-4'>
            <div className='flex flex-col gap-6'>
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

              <div className='grid gap-2'>
                <Label htmlFor='email' className='flex items-center'>
                  Ảnh sự kiện <p className='ml-1 text-gray-500'>(chọn ảnh chính)</p>
                </Label>

                <UploadFiles
                  files={files}
                  setFiles={setFiles}
                  mainImageIndex={mainImageIndex}
                  setMainImageIndex={setMainImageIndex}
                />
                {files.length == 0 ? (
                  <span className='text-[0.8rem] font-medium text-destructive'>{fileMessage}</span>
                ) : null}
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
                Trở lại
              </Button>
              <Button type='submit'>Chỉnh sửa sự kiện</Button>
            </div>
          </form>
        </Form>
      </CardContent>
    </>
  )
}
