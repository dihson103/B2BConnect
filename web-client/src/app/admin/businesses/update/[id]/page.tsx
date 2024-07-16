'use client'

import Image from 'next/image'
import { ChevronLeft, PlusCircle, Trash2, Upload } from 'lucide-react'

import { Badge } from '@/components/ui/badge'
import { Button } from '@/components/ui/button'
import { Card, CardContent, CardDescription, CardFooter, CardHeader, CardTitle } from '@/components/ui/card'
import { Input } from '@/components/ui/input'
import { Label } from '@/components/ui/label'
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select'
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from '@/components/ui/table'
import BranchTableRowAction from '@/app/admin/businesses/update/_components/button-action'
import { useState } from 'react'
import ImageDisplay from '@/components/image-choosed-display'

export default function UpdateBusinessPage() {
  const [avatarImage, setAvatarImage] = useState<File | null>(null)
  const [coverImage, setCoverImage] = useState<File | null>(null)

  const handleUploadAvatar = (e: React.ChangeEvent<HTMLInputElement>) => {
    setAvatarImage(e.target.files ? e.target.files[0] : null)
  }

  const handleUploadCover = (e: React.ChangeEvent<HTMLInputElement>) => {
    setCoverImage(e.target.files ? e.target.files[0] : null)
  }

  const handleDeleteAvatar = (index: number) => {
    setAvatarImage(null)
  }

  const handleDeleteCover = (index: number) => {
    setCoverImage(null)
  }

  return (
    <main className='grid flex-1 items-start gap-4 p-4 sm:px-6 sm:py-0 md:gap-8'>
      <div className='mx-auto grid max-w-[100rem] md:min-w-[50rem] lg:min-w-[70rem] flex-1 auto-rows-max gap-4'>
        <div className='flex items-center gap-4'>
          <Button variant='outline' size='icon' className='h-7 w-7'>
            <ChevronLeft className='h-4 w-4' />
            <span className='sr-only'>Back</span>
          </Button>
          <h1 className='flex-1 shrink-0 whitespace-nowrap text-xl font-semibold tracking-tight sm:grow-0'>
            Chỉnh sửa doanh nghiệp
          </h1>
          <Badge variant='outline' className='ml-auto sm:ml-0'>
            Chưa xác thực
          </Badge>
          <div className='hidden items-center gap-2 md:ml-auto md:flex'>
            <Button variant='outline' size='sm'>
              Quay lại
            </Button>
            <Button size='sm'>Lưu thay đổi</Button>
          </div>
        </div>
        <div className='grid gap-4 md:grid-cols-[1fr_250px] lg:grid-cols-3 lg:gap-8'>
          <div className='grid auto-rows-max items-start gap-4 lg:col-span-2 lg:gap-8'>
            <Card x-chunk='dashboard-07-chunk-0'>
              <CardHeader>
                <CardTitle>Thông tin tài khoản</CardTitle>
              </CardHeader>
              <CardContent>
                <div className='grid gap-6'>
                  <div className='grid gap-3'>
                    <Label htmlFor='email'>Email</Label>
                    <Input id='email' type='email' className='w-full' value={'dihson103@gmail.com'} />
                  </div>
                </div>
              </CardContent>
            </Card>
            <Card x-chunk='dashboard-07-chunk-1'>
              <CardHeader>
                <CardTitle>Thông tin doanh nghiệp</CardTitle>
              </CardHeader>
              <CardContent>
                <div className='grid gap-6'>
                  <div className='grid gap-3'>
                    <Label htmlFor='name'>Tên doanh nghiệp</Label>
                    <Input id='name' type='text' className='w-full' value={'Công ty cổ phần ABC'} />
                  </div>
                  <div className='grid grid-cols-1 md:grid-cols-2 gap-4'>
                    <div className='grid gap-3'>
                      <Label htmlFor='tax-code'>Mã số thuế</Label>
                      <Input id='tax-code' type='text' className='w-full' value={'000000000001'} />
                    </div>
                    <div className='grid gap-3'>
                      <Label htmlFor='tax-code'>Ngày thành lập</Label>
                      <Input id='tax-code' type='date' className='w-full' />
                    </div>
                  </div>
                  <div className='grid gap-3'>
                    <Label htmlFor='tax-code'>Web site</Label>
                    <Input id='tax-code' type='text' className='w-full' value={'abc-company.com'} />
                  </div>
                </div>
              </CardContent>
            </Card>
            <Card x-chunk='dashboard-07-chunk-2'>
              <CardHeader>
                <CardTitle>Thông tin người đại diện</CardTitle>
              </CardHeader>
              <CardContent>
                <div className='grid gap-6'>
                  <div className='grid grid-cols-1 md:grid-cols-2 gap-4'>
                    <div className='grid gap-3'>
                      <Label htmlFor='fullname'>Tên người đại diện</Label>
                      <Input id='fullname' type='text' className='w-full' value={'Nguyễn Đình Sơn'} />
                    </div>
                    <div className='grid gap-3'>
                      <Label htmlFor='cccd'>Căn cước công dân</Label>
                      <Input id='cccd' type='text' className='w-full' value={'000000000001'} />
                    </div>
                  </div>
                  <div className='grid grid-cols-1 md:grid-cols-2 gap-4'>
                    <div className='grid gap-3'>
                      <Label htmlFor='gender'>Giới tính</Label>
                      <Select>
                        <SelectTrigger id='gender' aria-label='Chọn giới tính'>
                          <SelectValue placeholder='Chọn giới tính' />
                        </SelectTrigger>
                        <SelectContent>
                          <SelectItem value='published'>Nam</SelectItem>
                          <SelectItem value='archived'>Nữ</SelectItem>
                        </SelectContent>
                      </Select>
                    </div>
                    <div className='grid gap-3'>
                      <Label htmlFor='dob'>Ngày sinh</Label>
                      <Input id='dob' type='date' className='w-full' />
                    </div>
                  </div>
                  <div className='grid gap-3'>
                    <Label htmlFor='address'>Địa chỉ</Label>
                    <Input
                      id='address'
                      type='text'
                      className='w-full'
                      value={'Vân Lũng - An Khánh - Hoài Đức - Hà Nội'}
                    />
                  </div>
                </div>
              </CardContent>
            </Card>
          </div>
          <div className='grid auto-rows-max items-start gap-4 lg:gap-8'>
            <Card x-chunk='dashboard-07-chunk-3'>
              <CardHeader>
                <CardTitle>Trạng thái tài khoản</CardTitle>
              </CardHeader>
              <CardContent>
                <div className='grid gap-6'>
                  <div className='grid gap-3'>
                    <Label htmlFor='status'>Trạng thái</Label>
                    <Select>
                      <SelectTrigger id='status' aria-label='Select status'>
                        <SelectValue placeholder='Select status' />
                      </SelectTrigger>
                      <SelectContent>
                        <SelectItem value='published'>Đang hoạt động</SelectItem>
                        <SelectItem value='archived'>Không còn hoạt động</SelectItem>
                      </SelectContent>
                    </Select>
                  </div>
                </div>
              </CardContent>
            </Card>
            <Card className='overflow-hidden' x-chunk='dashboard-07-chunk-4'>
              <CardHeader>
                <CardTitle>Ảnh đại diện của công ty</CardTitle>
              </CardHeader>
              <CardContent>
                {avatarImage === null ? (
                  <div className='w-full h-full flex justify-center items-center'>
                    <input
                      id='image'
                      type='file'
                      style={{ display: 'none' }}
                      accept='image/*'
                      onChange={(e) => handleUploadAvatar(e)}
                    />
                    <label htmlFor='image' className='flex flex-col items-center cursor-pointer'>
                      <Upload size={70} className='bg-primary-backgroudPrimary rounded-md p-5 mb-2' />
                      <span className='text-l text-gray-500 font-medium'>Hãy tải ảnh lên</span>
                    </label>
                  </div>
                ) : (
                  <div className='relative w-full h-full flex justify-center items-center'>
                    <ImageDisplay
                      imageURLs={[avatarImage].map((i) => URL.createObjectURL(i))}
                      handleDelete={handleDeleteAvatar}
                    />
                  </div>
                )}
              </CardContent>
            </Card>
            <Card x-chunk='dashboard-07-chunk-5'>
              <CardHeader>
                <CardTitle>Ảnh bìa doanh nghiệp</CardTitle>
              </CardHeader>
              <CardContent>
                {coverImage === null ? (
                  <div className='w-full h-full flex justify-center items-center'>
                    <input
                      id='image'
                      type='file'
                      style={{ display: 'none' }}
                      accept='image/*'
                      onChange={(e) => handleUploadCover(e)}
                    />
                    <label htmlFor='image' className='flex flex-col items-center cursor-pointer'>
                      <Upload size={70} className='bg-primary-backgroudPrimary rounded-md p-5 mb-2' />
                      <span className='text-l text-gray-500 font-medium'>Hãy tải ảnh lên</span>
                    </label>
                  </div>
                ) : (
                  <div className='relative w-full h-full flex justify-center items-center'>
                    <ImageDisplay
                      imageURLs={[coverImage].map((i) => URL.createObjectURL(i))}
                      handleDelete={handleDeleteCover}
                    />
                  </div>
                )}
              </CardContent>
            </Card>
          </div>
        </div>
        <Card x-chunk='dashboard-07-chunk-3'>
          <CardHeader>
            <CardTitle>Danh sách chi nhánh</CardTitle>
          </CardHeader>
          <CardContent>
            <Table>
              <TableHeader>
                <TableRow>
                  <TableHead className='w-[200px]'>Địa chỉ</TableHead>
                  <TableHead>Email</TableHead>
                  <TableHead>Số điện thoại</TableHead>
                  <TableHead>Loại trụ sở</TableHead>
                </TableRow>
              </TableHeader>
              <TableBody>
                <TableRow>
                  <TableCell className='font-semibold'>Vân Lũng - An Khánh - Hoài Đức - Hà Nội</TableCell>
                  <TableCell>dihson103@gmail.com</TableCell>
                  <TableCell>0976099351</TableCell>
                  <TableCell>
                    <Badge variant='outline' className='ml-auto sm:ml-0'>
                      Trụ sở chính
                    </Badge>
                  </TableCell>
                  <TableCell>
                    <BranchTableRowAction id='dfs' />
                  </TableCell>
                </TableRow>
                <TableRow>
                  <TableCell className='font-semibold'>Vân Lũng - An Khánh - Hoài Đức - Hà Nội</TableCell>
                  <TableCell>dihson103@gmail.com</TableCell>
                  <TableCell>0976099351</TableCell>
                  <TableCell>
                    <Badge variant='outline' className='ml-auto sm:ml-0'>
                      Trụ sở chính
                    </Badge>
                  </TableCell>
                  <TableCell>
                    <BranchTableRowAction id='dfs' />
                  </TableCell>
                </TableRow>
                <TableRow>
                  <TableCell className='font-semibold'>Vân Lũng - An Khánh - Hoài Đức - Hà Nội</TableCell>
                  <TableCell>dihson103@gmail.com</TableCell>
                  <TableCell>0976099351</TableCell>
                  <TableCell>
                    <Badge variant='outline' className='ml-auto sm:ml-0'>
                      Trụ sở chính
                    </Badge>
                  </TableCell>
                  <TableCell>
                    <BranchTableRowAction id='dfs' />
                  </TableCell>
                </TableRow>
              </TableBody>
            </Table>
          </CardContent>
          <CardFooter className='justify-center border-t p-4'>
            <Button size='sm' variant='ghost' className='gap-1'>
              <PlusCircle className='h-3.5 w-3.5' />
              Add Variant
            </Button>
          </CardFooter>
        </Card>
        <div className='flex items-center justify-center gap-2 md:hidden'>
          <Button variant='outline' size='sm'>
            Discard
          </Button>
          <Button size='sm'>Save Product</Button>
        </div>
      </div>
    </main>
  )
}
