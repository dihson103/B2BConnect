import { toast } from '@/components/ui/use-toast'
import { type ClassValue, clsx } from 'clsx'
import { twMerge } from 'tailwind-merge'

export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs))
}

export const apiErrorHandler = (error: string) => {
  toast({
    title: 'Lỗi',
    description: error ?? 'Lỗi không xác định',
    duration: 5000,
    variant: 'destructive'
  })
}
