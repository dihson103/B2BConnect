'use client'

import { Button } from '@/components/ui/button'
import { Checkbox } from '@/components/ui/checkbox'
import {
  Dialog,
  DialogClose,
  DialogContent,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger
} from '@/components/ui/dialog'
import { FormControl, FormField, FormItem, FormLabel, FormMessage } from '@/components/ui/form'
import { Input } from '@/components/ui/input'
import { CreateBranchFormType, CreateBranchSchema } from '@/rules/branch.rules'
import { zodResolver } from '@hookform/resolvers/zod'
import { PlusCircle } from 'lucide-react'
import { useState } from 'react'
import { FormProvider, useForm } from 'react-hook-form'

export default function CreateBranchForm() {
  const [open, setOpen] = useState(false)

  const form = useForm<CreateBranchFormType>({
    resolver: zodResolver(CreateBranchSchema),
    defaultValues: {
      email: '',
      address: '',
      phone: '',
      isMainBranch: false
    }
  })

  const onSubmit = (data: CreateBranchFormType) => {
    console.log(data)
    setOpen(false)
    form.reset()
  }

  return (
    <Dialog open={open} onOpenChange={setOpen}>
      <DialogTrigger asChild>
        <Button size='sm' variant='ghost' className='gap-1'>
          <PlusCircle className='h-3.5 w-3.5' />
          Thêm chi nhánh
        </Button>
      </DialogTrigger>
      <DialogContent className='sm:max-w-[500px]'>
        <DialogHeader>
          <DialogTitle>Tạo chi nhánh doanh nghiệp</DialogTitle>
        </DialogHeader>
        <FormProvider {...form}>
          <form onSubmit={form.handleSubmit(onSubmit)} className='w-full flex flex-col gap-4'>
            <div className='flex flex-col gap-6'>
              <FormField
                control={form.control}
                name='email'
                render={({ field }) => (
                  <FormItem>
                    <FormLabel className='text-primary-backgroudPrimary'>Email chi nhánh</FormLabel>
                    <FormControl>
                      <Input type='text' placeholder='Email' {...field} />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <FormField
                control={form.control}
                name='phone'
                render={({ field }) => (
                  <FormItem>
                    <FormLabel className='text-primary-backgroudPrimary'>Số điện thoại chi nhánh</FormLabel>
                    <FormControl>
                      <Input type='text' placeholder='Số điện thoại' {...field} />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <FormField
                control={form.control}
                name='address'
                render={({ field }) => (
                  <FormItem>
                    <FormLabel className='text-primary-backgroudPrimary'>Địa chỉ chi nhánh</FormLabel>
                    <FormControl>
                      <Input type='text' placeholder='Địa chỉ' {...field} />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <FormField
                control={form.control}
                name='isMainBranch'
                render={({ field }) => (
                  <FormItem>
                    <div className='flex items-center space-x-2'>
                      <Checkbox id='isMainBranch' checked={field.value} onCheckedChange={field.onChange} />
                      <label
                        htmlFor='isMainBranch'
                        className='text-sm font-medium leading-none peer-disabled:cursor-not-allowed peer-disabled:opacity-70'
                      >
                        Chi nhánh chính
                      </label>
                    </div>
                  </FormItem>
                )}
              />
            </div>
            <DialogFooter>
              <DialogClose asChild>
                <Button type='button' variant='secondary'>
                  Hủy
                </Button>
              </DialogClose>
              <Button type='submit'>Tạo chi nhánh</Button>
            </DialogFooter>
          </form>
        </FormProvider>
      </DialogContent>
    </Dialog>
  )
}
